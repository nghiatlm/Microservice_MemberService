
using MemberService.BO.Common;
using MemberService.BO.Entites;
using MemberService.BO.Enums;
using MemberService.DAO.DAO;
using Microsoft.EntityFrameworkCore;

namespace MemberService.Repository
{
    public class PackageTypeRepository : IPackageTypeRepository
    {
        public async Task<int> Add(PackageType profile) => await PackageTypeDAO.Instance.Add(profile);

        public async Task<int> Delete(PackageType profile) => await PackageTypeDAO.Instance.Delete(profile);

        public async Task<PackageType?> FindById(int id) => await PackageTypeDAO.Instance.FindById(id);

        public async Task<int> Update(PackageType profile) => await PackageTypeDAO.Instance.Update(profile);
        public async Task<PageResult<PackageType>> FindQueryParams(string? query, TypeLevel? Level = default, Status? Status = default, int pageNumber = 1, int pageSize = 10)
        {
            var search = PackageTypeDAO.Instance.FindQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                var q = query.Trim();
                search = search.Where(p => p.Name.Contains(q) || (p.Description != null && p.Description.Contains(q)));
            }
            if (Level.HasValue)
            {
                search = search.Where(p => p.Level == Level.Value);
            }
            if (Status.HasValue)
            {
                search = search.Where(p => p.Status == Status.Value);
            }

            var totalItems = await search.CountAsync();
            if (pageSize <= 0)
            {
                pageSize = totalItems == 0 ? 1 : (int)totalItems;
                pageNumber = 1;
            }

            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var items = await search
                .OrderByDescending(u => u.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PageResult<PackageType>(items, totalItems, totalPages, pageNumber, pageSize);
        }

        public async Task<PackageType?> FindByName(string name) => await PackageTypeDAO.Instance.FindByName(name);

        public async Task<PackageType?> FindByLevel(TypeLevel level) => await PackageTypeDAO.Instance.FindByLevel(level);
    }
}