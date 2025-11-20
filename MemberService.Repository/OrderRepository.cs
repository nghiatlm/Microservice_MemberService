using MemberService.BO.Common;
using MemberService.BO.Entites;
using MemberService.BO.Enums;
using MemberService.DAO.DAO;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MemberService.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public async Task<int> Add(Order entity) => await OrderDAO.Instance.Add(entity);

        public async Task<int> Delete(Order entity) => await OrderDAO.Instance.Delete(entity);

        public async Task<Order?> FindById(int id) => await OrderDAO.Instance.FindById(id);

        public async Task<int> Update(Order entity) => await OrderDAO.Instance.Update(entity);

        public async Task<PageResult<Order>> FindQueryParams(string? query, int? accountId = default, int? packageId = default, OrderStatus? orderStatus = default, int pageNumber = 1, int pageSize = 10)
        {
            var search = OrderDAO.Instance.FindQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                search = search.Where(o => o.TransactionCode.Contains(query) || (o.Notes != null && o.Notes.Contains(query)));
            }

            if (accountId.HasValue)
            {
                search = search.Where(o => o.AccountId == accountId.Value);
            }

            if (packageId.HasValue)
            {
                search = search.Where(o => o.PackageId == packageId.Value);
            }

            if (orderStatus.HasValue)
            {
                search = search.Where(o => o.OrderStatus == orderStatus.Value);
            }

            var totalItems = await search.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var items = await search
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResult<Order>(items, totalItems, totalPages, pageNumber, pageSize);
        }
    }
}
