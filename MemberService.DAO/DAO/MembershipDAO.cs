using System.Linq;
using System.Threading.Tasks;
using MemberService.BO.Entites;

namespace MemberService.DAO.DAO
{
    public class MembershipDAO
    {
        private MemberServiceDbContext _context;

        private static MembershipDAO _instance;

        public static MembershipDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MembershipDAO();
                }
                return _instance;
            }
        }

        public MembershipDAO()
        {
            _context = new MemberServiceDbContext();
        }

        public async Task<Membership?> FindById(int id) => await _context.Memberships.FindAsync(id);

        public IQueryable<Membership> FindQueryable() => _context.Memberships.AsQueryable();

        public async Task<int> Add(Membership entity)
        {
            _context.Memberships.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(Membership entity)
        {
            _context.Memberships.Update(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Membership entity)
        {
            _context.Memberships.Remove(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
