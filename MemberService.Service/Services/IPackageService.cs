
using MemberService.BO.Common;
using MemberService.BO.Entites;
using MemberService.BO.Enums;
using MemberService.BO.Requests;

namespace MemberService.Service.Services
{
    public interface IPackageService
    {
        Task<Package?> GetById(int id);
        Task<bool> Create(PackageRequest request);
        Task<bool> Update(int id, PackageRequest request);
        Task<bool> Delete(int id);
        Task<PageResult<Package>> GetPackageTypes(string? query, int? packageTypeId = default, Status? IsActive = default, int pageNumber = 1, int pageSize = 10);
    }
}