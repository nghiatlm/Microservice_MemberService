using MemberService.BO.Common;
using MemberService.BO.Entites;
using MemberService.BO.Requests;

namespace MemberService.Service.Services
{
    public interface IMemberService
    {
        Task<Membership> GetById(int id);
        Task<PageResult<Membership>> GetMembers(int? accountId = default, int? packageId = default, int pageNumber = 1, int pageSize = 10);
        Task<int> Update(Membership request);
        Task<int> Delete(int id);
    }
}
