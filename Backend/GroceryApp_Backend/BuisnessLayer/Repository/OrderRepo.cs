using AutoMapper;
using BuisnessLayer.IRepository;
using BuisnessLayer.Model;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public OrderRepo(AppDbContext appDbContext,IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }

        public async Task<List<OrderModel>> GetAllOrdersAsync(int userId)
        {
            var orderItems = await appDbContext.Orders.Where(x => x.UserId == userId).ToListAsync();
            var orders=mapper.Map<List<OrderModel>>(orderItems);
            return orders;
        }

        public List<int> GetTop5Orders(string month, int year)
        {
            var orders = appDbContext.Orders.Where(x => EF.Functions.Like(x.OrderDate, $"%{month}%") && EF.Functions.Like(x.OrderDate, $"%{year}%")).ToList();
            var productCounts =orders.GroupBy(x => x.ProductId).Select(g => new { ProductId = g.Key, Count = g.Count() }).ToList();
            var products = productCounts.OrderByDescending(x => x.Count).Take(5).Select(x => x.ProductId).ToList();

            return products;
        }

        public async Task PlaceOrderAsync(OrderModel order)
        {
            var orderEntity=mapper.Map<OrderEntity>(order);
            await appDbContext.Orders.AddAsync(orderEntity);
            await appDbContext.SaveChangesAsync();
        }

       
    }
}
