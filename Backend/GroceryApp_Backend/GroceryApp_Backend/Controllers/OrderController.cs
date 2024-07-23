using BuisnessLayer.IRepository;
using BuisnessLayer.Model;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroceryApp_Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class OrderController : Controller
    {
        //private readonly AppDbContext appDbContext;
        private readonly IOrderRepo orderRepo;

        public OrderController(/*AppDbContext appDbContext*/IOrderRepo orderRepo)
        {
            //this.appDbContext = appDbContext;
            this.orderRepo = orderRepo;
        }
        [HttpPost("placeOrder")]
        public async Task<IActionResult> PlaceOrder(OrderModel orderModel)
        {
            if (orderModel == null)
            {
                return BadRequest();
            }
            //await appDbContext.Orders.AddAsync(orderEntity);
            //await appDbContext.SaveChangesAsync();
            await orderRepo.PlaceOrderAsync(orderModel);
           
            
            return Ok(new { Message = "Order Added Successfully!!!" });
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetItemForUser([FromRoute] int userId)
        {
            //var orderItems = await appDbContext.Orders.Where(x => x.UserId == userId).ToListAsync();
            var orderItems=await orderRepo.GetAllOrdersAsync(userId);
            return Ok(orderItems);
        }
        [HttpGet("searchTop5Orders")]
        public IActionResult GetTopFiveOrders(string month,int year)
        {
            //var orders = await appDbContext.Orders.Where(x => EF.Functions.Like(x.OrderDate,$"%{month}%") && EF.Functions.Like(x.OrderDate,$"%{year}%")).ToListAsync();
            //var productCounts = orders.GroupBy(x => x.ProductId).Select(g => new { ProductId = g.Key, Count = g.Count() }).ToList();
            //var products=productCounts.OrderByDescending(x => x.Count).Take(5).Select(x=>x.ProductId).ToList();
            var products=orderRepo.GetTop5Orders(month,year);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
            
        }
    }
}
