using MemberService.BO.Common;
using MemberService.BO.Entites;
using MemberService.BO.Enums;
using MemberService.DAO.DAO;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MemberService.Repository
{
    public class PackageRepository : IPackageRepository
    {
        public async Task<int> Add(Package entity) => await PackageDAO.Instance.Add(entity);

        public async Task<int> Delete(Package entity) => await PackageDAO.Instance.Delete(entity);

        public async Task<Package?> FindById(int id) => await PackageDAO.Instance.FindById(id);

        public async Task<int> Update(Package entity) => await PackageDAO.Instance.Update(entity);

        public async Task<PageResult<Package>> FindQueryParams(string? query, int? packageTypeId = default, Status? IsActive = default, int pageNumber = 1, int pageSize = 10)
        {
            var search = PackageDAO.Instance.FindQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                search = search.Where(p => p.Name.Contains(query) || p.Code.Contains(query) || (p.Description != null && p.Description.Contains(query)));
            }

            if (packageTypeId.HasValue)
            {
                search = search.Where(p => p.PackageTypeId == packageTypeId.Value);
            }

            if (IsActive.HasValue)
            {
                search = search.Where(p => p.IsActive == IsActive.Value);
            }

            var totalItems = await search.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var items = await search
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResult<Package>(items, totalItems, totalPages, pageNumber, pageSize);
        }
    }
}
