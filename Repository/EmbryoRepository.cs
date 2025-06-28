namespace infertility_system.Repository
{
    using infertility_system.Data;
    using infertility_system.Interfaces;
    using infertility_system.Models;
    using Microsoft.EntityFrameworkCore;

    public class EmbryoRepository : IEmbryoRepository
    {
        private readonly AppDbContext context;

        public EmbryoRepository(AppDbContext context, IAuthService authService)
        {
            this.context = context;
        }

        public async Task<ICollection<Embryo>> GetListEmbryosAsync(int userId)
        {
            var embryos = await this.context.Embryos.Where(x => x.Customer.UserId == userId).ToListAsync();
            return embryos;
        }
    }
}
