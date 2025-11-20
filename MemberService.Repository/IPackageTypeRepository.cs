

using MemberService.BO.Common;
using MemberService.BO.Entites;
using MemberService.BO.Enums;

namespace MemberService.Repository
{
    public interface IPackageTypeRepository
    {
        Task<PackageType?> FindById(int id);
        Task<PackageType?> FindByName(string name);
        Task<PackageType?> FindByLevel(TypeLevel level);
        Task<int> Add(PackageType profile);
        Task<int> Update(PackageType profile);
        Task<int> Delete(PackageType profile);
        Task<PageResult<PackageType>> FindQueryParams(string? query, TypeLevel? Level = default, Status? Status = default, int pageNumber = 1, int pageSize = 10);
    }
}