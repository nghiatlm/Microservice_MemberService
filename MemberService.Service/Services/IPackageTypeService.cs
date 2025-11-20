using MemberService.BO.Common;
using MemberService.BO.Entites;
using MemberService.BO.Enums;
using MemberService.BO.Requests;

namespace MemberService.Service.Services
{
    public interface IPackageTypeService
    {
        Task<PackageType?> GetById(int id);
        Task<bool> Create(PackageTypeRequest request);
        Task<bool> Update(int id, PackageTypeRequest request);
        Task<bool> Delete(int id);
        Task<PageResult<PackageType>> GetPackageTypes(string? query, TypeLevel? Level = default, Status? Status = default, int pageNumber = 1, int pageSize = 10);
    }
}