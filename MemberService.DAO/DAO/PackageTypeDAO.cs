
using MemberService.BO.Entites;
using MemberService.BO.Enums;
using Microsoft.EntityFrameworkCore;

namespace MemberService.DAO.DAO
{
    public class PackageTypeDAO
    {
        private MemberServiceDbContext _context;

        private static PackageTypeDAO _instance;

        public static PackageTypeDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PackageTypeDAO();
                }
                return _instance;
            }
        }

        public PackageTypeDAO()
        {
            _context = new MemberServiceDbContext();
        }

        public async Task<PackageType?> FindById(int id) => await _context.PackageTypes.FindAsync(id);
        public async Task<PackageType?> FindByName(string name) => await _context.PackageTypes
            .FirstOrDefaultAsync(pt => pt.Name == name);

        public async Task<PackageType?> FindByLevel(TypeLevel level) => await _context.PackageTypes
            .FirstOrDefaultAsync(pt => pt.Level == level);

        public IQueryable<PackageType> FindQueryable() => _context.PackageTypes.AsQueryable();

        public async Task<int> Add(PackageType profile)
        {
            _context.PackageTypes.Add(profile);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(PackageType profile)
        {
            _context.PackageTypes.Update(profile);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(PackageType profile)
        {
            _context.PackageTypes.Remove(profile);
            return await _context.SaveChangesAsync();
        }
    }
}