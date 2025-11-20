using MemberService.BO.Common;
using MemberService.BO.Entites;
using MemberService.BO.Enums;
using MemberService.DAO.DAO;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MemberService.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        public async Task<int> Add(Payment entity) => await PaymentDAO.Instance.Add(entity);

        public async Task<int> Delete(Payment entity) => await PaymentDAO.Instance.Delete(entity);

        public async Task<Payment?> FindById(int id) => await PaymentDAO.Instance.FindById(id);

        public async Task<int> Update(Payment entity) => await PaymentDAO.Instance.Update(entity);

        public async Task<PageResult<Payment>> FindQueryParams(int? orderId = default, PaymentStatus? paymentStatus = default, PaymentMethod? method = default, int pageNumber = 1, int pageSize = 10)
        {
            var search = PaymentDAO.Instance.FindQueryable();

            if (orderId.HasValue)
            {
                search = search.Where(p => p.OrderId == orderId.Value);
            }

            if (paymentStatus.HasValue)
            {
                search = search.Where(p => p.PaymentStatus == paymentStatus.Value);
            }

            if (method.HasValue)
            {
                search = search.Where(p => p.PaymentMethod == method.Value);
            }

            var totalItems = await search.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var items = await search
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResult<Payment>(items, totalItems, totalPages, pageNumber, pageSize);
        }
    }
}
