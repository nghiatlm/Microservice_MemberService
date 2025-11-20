using System.Linq;
using System.Threading.Tasks;
using MemberService.BO.Entites;

namespace MemberService.DAO.DAO
{
    public class PackageDAO
    {
        private MemberServiceDbContext _context;

        private static PackageDAO _instance;

        public static PackageDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PackageDAO();
                }
                return _instance;
            }
        }

        public PackageDAO()
        {
            _context = new MemberServiceDbContext();
        }

        public async Task<Package?> FindById(int id) => await _context.Packages.FindAsync(id);

        public IQueryable<Package> FindQueryable() => _context.Packages.AsQueryable();

        public async Task<int> Add(Package entity)
        {
            _context.Packages.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(Package entity)
        {
            _context.Packages.Update(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Package entity)
        {
            _context.Packages.Remove(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
