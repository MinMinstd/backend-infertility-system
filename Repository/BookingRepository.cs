using AutoMapper;
using infertility_system.Data;
using infertility_system.Dtos.Booking;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace infertility_system.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BookingRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> BookingServiceAsync(BookingDto dto)
        {
            var booking = _mapper.Map<Booking>(dto);
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            var order = _mapper.Map<Order>(dto);
            order.BookingId = booking.BookingId;
            order.Wife = dto.Wife;
            order.Husband = dto.Husband;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.DoctorId == dto.DoctorId);
            if(doctor == null)
                return false;

            var orderDetail = new OrderDetail
            {
                OrderId = order.OrderId,
                DoctorName = doctor.FullName
            };
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckCustomerInBookingAsync(int customerId)
        {
            return await _context.Bookings.AnyAsync(x => x.CustomerId == customerId);
        }
    }
}

