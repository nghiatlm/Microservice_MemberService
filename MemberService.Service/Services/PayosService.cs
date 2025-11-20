
using System.Net;
using MemberService.BO.Entites;
using MemberService.BO.Enums;
using MemberService.BO.Exceptions;
using MemberService.Repository;
using Microsoft.Extensions.Logging;
using Net.payOS;
using Net.payOS.Types;

namespace MemberService.Service.Services
{
    public class PayosService : IPayosService
    {
        private readonly PayOS _payOs;
        private readonly IOrderRepository _orderRepository;
        private readonly IPackageRepository _packageRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly ILogger<PayosService> _logger;

        public PayosService(PayOS payOs, IOrderRepository orderRepository, IPackageRepository packageRepository, IPaymentRepository paymentRepository, IMembershipRepository membershipRepository, ILogger<PayosService> logger)
        {
            _payOs = payOs;
            _orderRepository = orderRepository;
            _packageRepository = packageRepository;
            _paymentRepository = paymentRepository;
            _membershipRepository = membershipRepository;
            _logger = logger;
        }

        public async Task<string> CreatePaymentAsync(int id)
        {
            try
            {
                var order = await _orderRepository.FindById(id);
                if (order == null) throw new AppException("Order not found", HttpStatusCode.NotFound);
                var orderCode = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                var payment = new Payment
                {
                    OrderId = order.Id,
                    Amount = order.TotalAmount,
                    PaymentMethod = PaymentMethod.PAY_OS,
                    PaymentStatus = PaymentStatus.PENDING,
                    TransactionCode = orderCode.ToString()
                };
                var result = await _paymentRepository.Add(payment);
                if (result <= 0) throw new AppException("Create payment failed", HttpStatusCode.InternalServerError);
                ItemData item = new ItemData(order?.Package?.Name ?? "Unknown Package", 1, (int)(order?.Package?.Price ?? 0));
                List<ItemData> items = new List<ItemData> { item };
                PaymentData paymentData = new PaymentData(
                    orderCode,
                    (int)(order?.Package?.Price ?? 0),
                    "Đơn hàng phục vụ học tập",
                    items,
                    $"https://modernestate.vercel.app/create-post-failure",
                    $"https://modernestate.vercel.app/create-post-success"
                );
                CreatePaymentResult createPayment = await _payOs.createPaymentLink(paymentData);
                return createPayment.checkoutUrl;
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {Message}", ex.Message);
                throw new AppException("Internal Server Error", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<bool> VerifyPaymentAsync(WebhookType type)
        {
            try
            {
                var data = _payOs.verifyPaymentWebhookData(type);
                var payment = await _paymentRepository.FindByCode(data.orderCode.ToString());
                if (payment == null) return true;
                var order = payment.Order ?? await _orderRepository.FindById(payment.OrderId);
                if (data.code == "00")
                {
                    payment.PaymentStatus = PaymentStatus.SUCCESS;
                    if (order != null) order.OrderStatus = OrderStatus.SUCCESS;
                    await CreateMemberShip(order);
                }
                else
                {
                    payment.PaymentStatus = PaymentStatus.FAILED;
                    if (order != null) order.OrderStatus = OrderStatus.FAILED;
                }
                await _paymentRepository.Update(payment);
                if (order != null) await _orderRepository.Update(order);
                return true;
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {Message}", ex.Message);
                throw new AppException("Internal Server Error", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<bool> CreateMemberShip(Order order)
        {
            try
            {
                var package = await _packageRepository.FindById(order.PackageId);
                if (package == null) throw new AppException("Package not found", HttpStatusCode.NotFound);
                var membership = new Membership
                {
                    AccountId = order.AccountId,
                    PackageId = order.PackageId,
                    PurchaseDate = order.OrderDate,
                    StartDate = order.OrderDate,
                    LevelAtPurchase = package.PackageType.Level,
                    EndDate = DateTime.UtcNow.AddMonths(package.DurationInDays),
                    PriceAtPurchase = package.Price,
                    Status = order.OrderStatus == OrderStatus.SUCCESS ? MembershipStatus.ACTIVCE : MembershipStatus.CANCELLED,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                var result = await _membershipRepository.Add(membership);
                if (result <= 0) throw new AppException("Create membership failed", HttpStatusCode.InternalServerError);
                return true;
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {Message}", ex.Message);
                throw new AppException("Internal Server Error", HttpStatusCode.InternalServerError);
            }
        }
    }
}