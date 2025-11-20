

using System.Net;
using MemberService.BO.Common;
using MemberService.BO.Entites;
using MemberService.BO.Enums;
using MemberService.BO.Exceptions;
using MemberService.BO.Requests;
using MemberService.Repository;
using Microsoft.Extensions.Logging;

namespace MemberService.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPackageRepository _packageRepository;
        private readonly IPayosService _payosService;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository, IPackageRepository packageRepository, IPayosService payosService, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _packageRepository = packageRepository;
            _payosService = payosService;
            _logger = logger;
        }

        public async Task<string> Create(OrderRequest request)
        {
            try
            {
                var package = await _packageRepository.FindById(request.PackageId);
                if (package == null) throw new AppException("Package not found", HttpStatusCode.NotFound);
                var order = new Order
                {
                    OrderDate = DateTime.UtcNow,
                    AccountId = request.AccountId,
                    TotalAmount = request.TotalAmount == 0 ? request.TotalAmount : package.Price,
                    PackageId = request.PackageId,
                    OrderStatus = OrderStatus.PENDING,
                    Notes = request.Notes,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                var result = await _orderRepository.Add(order);
                if (result < 0) throw new AppException("Create order failed", HttpStatusCode.InternalServerError);
                var paymentUrl = await _payosService.CreatePaymentAsync(order.Id);
                return paymentUrl != null ? paymentUrl : null;
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

        public async Task<Order> GetById(int id)
        {
            try
            {
                var order = await _orderRepository.FindById(id);
                if (order == null) throw new AppException("Order not found", HttpStatusCode.NotFound);
                return order ?? throw new AppException("Order not found", HttpStatusCode.NotFound);
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

        public async Task<PageResult<Order>> GetOrders(string? query, int? accountId = null, int? packageId = null, int? orderStatus = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var result = await _orderRepository.FindQueryParams(query, accountId, packageId, orderStatus.HasValue ? (OrderStatus?)orderStatus : null, pageNumber, pageSize);
                return result ?? new PageResult<Order>(new List<Order>(), 0, 0, pageNumber, pageSize);
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