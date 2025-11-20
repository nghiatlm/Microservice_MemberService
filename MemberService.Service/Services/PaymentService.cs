using System.Net;
using MemberService.BO.Common;
using MemberService.BO.Entites;
using MemberService.BO.Enums;
using MemberService.BO.Exceptions;
using MemberService.Repository;
using Microsoft.Extensions.Logging;

namespace MemberService.Service.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IPaymentRepository paymentRepository, ILogger<PaymentService> logger)
        {
            _paymentRepository = paymentRepository;
            _logger = logger;
        }

        public async Task<Payment> GetById(int id)
        {
            try
            {
                var payment = await _paymentRepository.FindById(id);
                if (payment == null) throw new AppException("Payment not found", HttpStatusCode.NotFound);
                return payment;
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching payment: {Message}", ex.Message);
                throw new AppException("Internal Server Error", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<PageResult<Payment>> GetPayments(int? orderId = default, PaymentStatus? paymentStatus = default, PaymentMethod? method = default, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var result = await _paymentRepository.FindQueryParams(orderId, paymentStatus, method, pageNumber, pageSize);
                return result ?? new PageResult<Payment>(new List<Payment>(), 0, 0, pageNumber, pageSize);
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching payments: {Message}", ex.Message);
                throw new AppException("Internal Server Error", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<int> Update(Payment request)
        {
            try
            {
                var existing = await _paymentRepository.FindById(request.Id);
                if (existing == null) throw new AppException("Payment not found", HttpStatusCode.NotFound);

                // map updatable fields
                existing.TransactionCode = request.TransactionCode;
                existing.PaymentStatus = request.PaymentStatus;
                existing.PaymentMethod = request.PaymentMethod;
                existing.Amount = request.Amount;
                existing.UpdatedAt = DateTime.UtcNow;

                var updated = await _paymentRepository.Update(existing);
                if (updated < 0) throw new AppException("Update failed", HttpStatusCode.InternalServerError);
                return updated;
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating payment: {Message}", ex.Message);
                throw new AppException("Internal Server Error", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                var existing = await _paymentRepository.FindById(id);
                if (existing == null) throw new AppException("Payment not found", HttpStatusCode.NotFound);
                var deleted = await _paymentRepository.Delete(existing);
                if (deleted < 0) throw new AppException("Delete failed", HttpStatusCode.InternalServerError);
                return deleted;
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting payment: {Message}", ex.Message);
                throw new AppException("Internal Server Error", HttpStatusCode.InternalServerError);
            }
        }
    }
}
