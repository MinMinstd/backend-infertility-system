namespace infertility_system.Repository
{
    using System.Collections.Generic;
    using infertility_system.Data;
    using infertility_system.Dtos.Embryo;
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

        public async Task<bool> CreateEmbryoAsync(CreateEmbryoDto dto, int bookingId)
        {
            var order = await this.context.Orders
                .FirstOrDefaultAsync(o => o.BookingId == bookingId);

            var embryo = new Embryo()
            {
                CreateAt = dto.CreateAt,
                Quality = dto.Quality,
                Type = "NA",
                Status = dto.Status,
                Note = dto.Note,
                OrderId = order.OrderId,
            };
            this.context.Embryos.Add(embryo);
            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Embryo>> GetEmbryosInDoctorAsync(int bookingId, int customerId)
        {
            var embryoResult = new List<Embryo>();

            //lấy order hiện tại
            var currentOrder = await this.context.Orders
                .FirstOrDefaultAsync(o => o.BookingId == bookingId);

            List<Embryo> embryoCurrent = new List<Embryo>();
            if (currentOrder != null)
            {
                embryoCurrent = await this.context.Embryos
                    .Where(e => e.OrderId == currentOrder.OrderId && e.Status == "Đạt chuẩn")
                    .ToListAsync();
            }

            embryoResult.AddRange(embryoCurrent);

            var previousBookingIds = await this.context.Bookings
                .Where(b => b.CustomerId == customerId && b.BookingId < bookingId)
                .Select(b => b.BookingId)
                .ToListAsync();

            var previousOrderIds = await this.context.Orders
                .Where(o => previousBookingIds.Contains(o.OrderId))
                .Select(o => o.OrderId)
                .ToListAsync();

            var previousEmbryos = await this.context.Embryos
                .Where(e => previousOrderIds.Contains(e.OrderId) && e.Status == "Đạt chuẩn")
                .ToListAsync();

            embryoResult.AddRange(previousEmbryos);

            return embryoResult;
        }

        public async Task<ICollection<Embryo>> GetListEmbryosAsync(int userId)
        {
            var customer = await this.context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);

            var booking = await this.context.Bookings
                        .Where(x => x.CustomerId == customer.CustomerId)
                        .ToListAsync();

            var bookingId = booking.Select(b => b.BookingId).Distinct().ToList();

            var order = await this.context.Orders
                        .Where(o => bookingId.Contains((int)o.BookingId))
                        .ToListAsync();

            var orderIds = order.Select(o => o.OrderId).Distinct().ToList();

            var embryos = await this.context.Embryos
                        .Where(e => orderIds.Contains(e.OrderId) && e.Status == "Đạt chuẩn")
                        .ToListAsync();
            return embryos;
        }

        public async Task<bool> UpdateEmbryoAsync(UpdateEmbryoDto dto, int embryoId)
        {
            var embryo = await this.context.Embryos.FirstOrDefaultAsync(e => e.EmbryoId == embryoId);

            embryo.TransferredAt = dto.TransferredAt;
            embryo.Type = dto.Type;
            embryo.Status = dto.Status;
            embryo.Note = dto.Note;
            await this.context.SaveChangesAsync();
            return true;
        }
    }
}
