using MemberService.BO.Common;
using MemberService.BO.Entites;
using MemberService.BO.Enums;

namespace MemberService.Repository
{
    public interface IPackageRepository
    {
        Task<Package?> FindById(int id);
        Task<int> Add(Package entity);
        Task<int> Update(Package entity);
        Task<int> Delete(Package entity);
        Task<PageResult<Package>> FindQueryParams(string? query, int? packageTypeId = default, Status? IsActive = default, int pageNumber = 1, int pageSize = 10);
    }
}
