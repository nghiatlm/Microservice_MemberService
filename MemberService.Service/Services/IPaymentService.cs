using MemberService.BO.Common;
using MemberService.BO.Entites;
using MemberService.BO.Enums;

namespace MemberService.Service.Services
{
    public interface IPaymentService
    {
        Task<Payment> GetById(int id);
        Task<PageResult<Payment>> GetPayments(int? orderId = default, PaymentStatus? paymentStatus = default, PaymentMethod? method = default, int pageNumber = 1, int pageSize = 10);
        Task<int> Update(Payment request);
        Task<int> Delete(int id);
    }
}
