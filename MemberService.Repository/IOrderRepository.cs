using MemberService.BO.Common;
using MemberService.BO.Entites;
using MemberService.BO.Enums;

namespace MemberService.Repository
{
    public interface IOrderRepository
    {
        Task<Order?> FindById(int id);
        Task<int> Add(Order entity);
        Task<int> Update(Order entity);
        Task<int> Delete(Order entity);
        Task<PageResult<Order>> FindQueryParams(string? query, int? accountId = default, int? packageId = default, OrderStatus? orderStatus = default, int pageNumber = 1, int pageSize = 10);
    }
}
