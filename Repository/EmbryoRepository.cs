using infertility_system.Data;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.EntityFrameworkCore;

namespace infertility_system.Repository
{
    public class EmbryoRepository : IEmbryoRepository
    {
        private readonly AppDbContext _context;
        public EmbryoRepository(AppDbContext context, IAuthService authService)
        {
            _context = context;
        }
        public async Task<ICollection<Embryo>> GetListEmbryosAsync(int userId)
        {
            var embryos = await _context.Embryos.Where(x => x.Customer.UserId == userId).ToListAsync();
            return embryos;
        }
    }
}
