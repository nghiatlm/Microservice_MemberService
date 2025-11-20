using System.Linq;
using System.Threading.Tasks;
using MemberService.BO.Entites;
using Microsoft.EntityFrameworkCore;

namespace MemberService.DAO.DAO
{
    public class OrderDAO
    {
        private MemberServiceDbContext _context;

        private static OrderDAO _instance;

        public static OrderDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OrderDAO();
                }
                return _instance;
            }
        }

        public OrderDAO()
        {
            _context = new MemberServiceDbContext();
        }

        public async Task<Order?> FindById(int id) => await _context.Orders.Include(o => o.Package).ThenInclude(o => o.PackageType).FirstOrDefaultAsync(o => o.Id == id);

        public IQueryable<Order> FindQueryable() => _context.Orders.Include(o => o.Package).ThenInclude(o => o.PackageType).AsQueryable();

        public async Task<int> Add(Order entity)
        {
            _context.Orders.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(Order entity)
        {
            _context.Orders.Update(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Order entity)
        {
            _context.Orders.Remove(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
