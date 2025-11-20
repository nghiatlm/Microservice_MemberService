using System.Linq;
using System.Threading.Tasks;
using MemberService.BO.Entites;

namespace MemberService.DAO.DAO
{
    public class PaymentDAO
    {
        private MemberServiceDbContext _context;

        private static PaymentDAO _instance;

        public static PaymentDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PaymentDAO();
                }
                return _instance;
            }
        }

        public PaymentDAO()
        {
            _context = new MemberServiceDbContext();
        }

        public async Task<Payment?> FindById(int id) => await _context.Payments.FindAsync(id);

        public IQueryable<Payment> FindQueryable() => _context.Payments.AsQueryable();

        public async Task<int> Add(Payment entity)
        {
            _context.Payments.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(Payment entity)
        {
            _context.Payments.Update(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Payment entity)
        {
            _context.Payments.Remove(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
