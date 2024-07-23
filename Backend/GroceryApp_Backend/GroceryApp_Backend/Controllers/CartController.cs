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
    public class CartController : Controller
    {
        //private readonly AppDbContext appDbContext;
        private readonly ICartRepo cartRepo;

        public CartController(/*AppDbContext appDbContext*/ICartRepo cartRepo)
        {
            //this.appDbContext = appDbContext;
            this.cartRepo = cartRepo;
        }
        [HttpPost("addToCart")]
        public async Task<IActionResult> AddToCart(CartModel cartModel)
        {
            if (cartModel == null)
            {
                return BadRequest();
            }
            //await appDbContext.Carts.AddAsync(cartEntity);
            //await appDbContext.SaveChangesAsync();
            await cartRepo.AddToCartAsync(cartModel);
            return Ok("Item Added To Cart Successfully!!!");
        }
        [HttpGet("getAllItemsInCart")]
        public async Task<IActionResult> GetAllItemsInCart()
        {
            //var cartItems = await appDbContext.Carts.ToListAsync();
            var cartItems=await cartRepo.GetAllAsync();
            return Ok(cartItems);
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetItemForUser([FromRoute] int userId)
        {
            //var cartItems=await appDbContext.Carts.Where(x=>x.UserId== userId).ToListAsync();
            var cartItems=await cartRepo.GetItemsFromUserAsync(userId);
            return Ok(cartItems);
        }
        [HttpPut("updateQuantity")]
        public async Task<IActionResult> UpdateQuantity(CartModel cartModel)
        {
            //var product = await appDbContext.Carts.FirstOrDefaultAsync(x => x.UserId == cartEntity.UserId && x.ProductId == cartEntity.ProductId);
            //if(product == null)
            //{
            //    return NotFound();
            //}
            //product.Quantity = cartEntity.Quantity;
            //appDbContext.SaveChanges();
            //return Ok(new {Message="Quantity changed successfully!!!"});
            var result=await cartRepo.UpdateQuantityAsync(cartModel);
            if (result == "null")
            {
                return NotFound();
            }
            else
            {
                return Ok(new { Message = "Quantity changed successfully!!!" });
            }
        }
        [HttpPost("checkProductInCart")]
        public async Task<IActionResult> CheckProductInCart([FromBody]CartModel cartModel)
        {
            //var product = await appDbContext.Carts.FirstOrDefaultAsync(x => x.UserId == cartEntity.UserId && x.ProductId == cartEntity.ProductId);
            var product = await cartRepo.CheckProductInCartAsync(cartModel);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(new {Message="Item already in cart"});
        }
        [HttpDelete("removeAllItemFromCart/{userId}")]
        public async Task<IActionResult> removeAllItemFromCart([FromRoute] int userId)
        {
            //var cartItems=await appDbContext.Carts.Where(x=>x.UserId== userId).ToListAsync();
            //if(cartItems.Count ==0) {
            //    return NotFound();
            //}
            //appDbContext.Carts.RemoveRange(cartItems);
            //await appDbContext.SaveChangesAsync();
            //return Ok(new { Message = "Cart is empty now" });
            var result=await cartRepo.RemoveAllItemsFromCartAsync(userId);
            if (result == "null")
            {
                return BadRequest();
            }
            else
            {
                return Ok(new { Message = "Cart is empty now" });
            }
        }
        [HttpPut("updateAvailableQuantity")]
        public async Task<IActionResult> UpdateAvailableQuantity(ProductModel productModel)
        {
            //var product=await appDbContext.Products.FirstOrDefaultAsync(x=>x.Id==productEntity.Id); ;
            //if(product == null)
            //{
            //    return NotFound();
            //}
            //product.AvailableQuanity=productEntity.AvailableQuanity;
            //if(productEntity.AvailableQuanity < 0)
            //{
            //    product.AvailableQuanity = 0;
            //}
            //await appDbContext.SaveChangesAsync();
            //return Ok("Updated Quantity Successfully");
            var result = await cartRepo.UpdateAvailableQuanityAsync(productModel);
            if (result == "null")
            {
                return BadRequest();
            }
            else
            {
                return Ok("Updated Quantity Successfully");
            }
        }
        [HttpDelete("removeFromCart/{cartId}")]
        public async Task<IActionResult> RemoveFromCart([FromRoute] int cartId)
        {
            //var cartItem=await appDbContext.Carts.FirstOrDefaultAsync(x=>x.CartId==cartId);
            //if(cartItem == null)
            //{
            //    return NotFound();
            //}
            //appDbContext.Carts.Remove(cartItem);
            //await appDbContext.SaveChangesAsync();
            //return Ok(new { Message = "Removed Successfully!!!" });
            var result=await cartRepo.RemoveFromCartAsync(cartId);
            if (result == "null")
            {
                return BadRequest();
            }
            else
            {
                return Ok(new { Message = "Removed Successfully!!!" });
            }
        }
    }
}
