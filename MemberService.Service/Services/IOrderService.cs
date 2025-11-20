
using MemberService.BO.Common;
using MemberService.BO.Entites;
using MemberService.BO.Requests;

namespace MemberService.Service.Services
{
    public interface IOrderService
    {
        Task<string> Create(OrderRequest request);
        Task<Order> GetById(int id);
        Task<PageResult<Order>> GetOrders(string? query, int? accountId = default, int? packageId = default, int? orderStatus = default, int pageNumber = 1, int pageSize = 10);
    }
}