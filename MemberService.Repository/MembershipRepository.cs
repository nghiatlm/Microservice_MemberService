using MemberService.BO.Common;
using MemberService.BO.Entites;
using MemberService.BO.Enums;
using MemberService.DAO.DAO;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MemberService.Repository
{
    public class MembershipRepository : IMembershipRepository
    {
        public async Task<int> Add(Membership entity) => await MembershipDAO.Instance.Add(entity);

        public async Task<int> Delete(Membership entity) => await MembershipDAO.Instance.Delete(entity);

        public async Task<Membership?> FindById(int id) => await MembershipDAO.Instance.FindById(id);

        public async Task<int> Update(Membership entity) => await MembershipDAO.Instance.Update(entity);

        public async Task<PageResult<Membership>> FindQueryParams(int? accountId = default, int? packageId = default, MembershipStatus? status = default, int pageNumber = 1, int pageSize = 10)
        {
            var search = MembershipDAO.Instance.FindQueryable();

            if (accountId.HasValue)
            {
                search = search.Where(m => m.AccountId == accountId.Value);
            }

            if (packageId.HasValue)
            {
                search = search.Where(m => m.PackageId == packageId.Value);
            }

            if (status.HasValue)
            {
                search = search.Where(m => m.Status == status.Value);
            }

            var totalItems = await search.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var items = await search
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResult<Membership>(items, totalItems, totalPages, pageNumber, pageSize);
        }
    }
}
