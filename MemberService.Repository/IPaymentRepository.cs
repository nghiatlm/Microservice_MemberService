using MemberService.BO.Common;
using MemberService.BO.Entites;
using MemberService.BO.Enums;

namespace MemberService.Repository
{
    public interface IPaymentRepository
    {
        Task<Payment?> FindById(int id);
        Task<Payment?> FindByCode(string code);
        Task<int> Add(Payment entity);
        Task<int> Update(Payment entity);
        Task<int> Delete(Payment entity);
        Task<PageResult<Payment>> FindQueryParams(int? orderId = default, PaymentStatus? paymentStatus = default, PaymentMethod? method = default, int pageNumber = 1, int pageSize = 10);
    }
}
