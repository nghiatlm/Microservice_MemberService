using MemberService.BO.Common;
using MemberService.BO.Entites;
using MemberService.BO.Enums;

namespace MemberService.Repository
{
    public interface IMembershipRepository
    {
        Task<Membership?> FindById(int id);
        Task<int> Add(Membership entity);
        Task<int> Update(Membership entity);
        Task<int> Delete(Membership entity);
        Task<PageResult<Membership>> FindQueryParams(int? accountId = default, int? packageId = default, MembershipStatus? status = default, int pageNumber = 1, int pageSize = 10);
    }
}
