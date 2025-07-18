﻿namespace infertility_system.Repository
{
    using infertility_system.Data;
    using infertility_system.Interfaces;
    using infertility_system.Models;
    using Microsoft.EntityFrameworkCore;

    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly AppDbContext _context;

        public OrderDetailRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDetail> CreateOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();
            return orderDetail;
        }

        public async Task<List<OrderDetail>> GetListOrderDetailByOrderId(int OrdersId)
        {
            return await _context.OrderDetails.Where(x => x.OrderId == OrdersId && !string.IsNullOrEmpty(x.StageName) && !string.IsNullOrEmpty(x.TimeTreatment)).ToListAsync();
        }
    }
}
