using BuisnessLayer.IRepository;
using BuisnessLayer.Model;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace GroceryApp_Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductController : Controller
    {
        //private readonly AppDbContext appDbContext;
        private readonly IProductRepo productRepo;

        public ProductController(/*AppDbContext appDbContext*/IProductRepo productRepo)
        {
           // this.appDbContext = appDbContext;
            this.productRepo = productRepo;
        }
        [HttpPost("addProduct")]
        [Authorize]
        public async Task<IActionResult> AddProduct(ProductModel product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            //await appDbContext.Products.AddAsync(product);
            //await appDbContext.SaveChangesAsync();
            await productRepo.AddProductAsync(product);
            return Ok("Product Added Successfully!!!");
        }
        [HttpGet("getProductOnSearch")]
        public async Task<IActionResult> GetProductByName([FromQuery] string ProductName)
        {
            //var product = await appDbContext.Products.Where(x => x.ProductName == ProductName).ToListAsync();
            var product=await productRepo.GetProductByNameAsync(ProductName);
            if (product == null)
            {
                return NotFound(new { Message = "No Product found" });
            }
            return Ok(product);
        }
        [HttpGet("getAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            //var product = await appDbContext.Products.ToListAsync();
            var product= await productRepo.GetAllProductAsync();
            if (product == null)
            {
                return NotFound(new { Message = "Products not found" });
            }
            return Ok(product);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            //var product = await appDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            var product=await productRepo.GetProductByIdAsync(id);
            if (product == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Product not found" });
            }
            return Ok(product);
        }
        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id,ProductModel product)
        {
            //var productDetails = await appDbContext.Products.FindAsync(id);
            //if (productDetails == null)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Product details not found" });
            //}
            //productDetails.ProductName=product.ProductName;
            //productDetails.Description=product.Description;
            //productDetails.Category=product.Category;
            //productDetails.Price=product.Price;
            //productDetails.AvailableQuanity=product.AvailableQuanity;
            //productDetails.ImageLink=product.ImageLink;
            //productDetails.Specification=product.Specification;
            //await appDbContext.SaveChangesAsync();
            //return Ok(new { Message = "Updated Successfully" });
            var result = await productRepo.UpdateProductAsync(id, product);
            if(result == "Product Not Found")
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Product details not found" });
            }
            else
            {
                return Ok(new { Message = result });
            }
        }
        [HttpDelete]
        [Route("deleteProduct/{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            //var product = await appDbContext.Products.FindAsync(id);
            //if (product == null)
            //{
            //    return NotFound(new { Message = "Product not found" });
            //}
            //appDbContext.Products.Remove(product);
            //await appDbContext.SaveChangesAsync();
            //return Ok(new { Message = "Deleted Successfully" });
            var result=await productRepo.DeleteProductAsync(id);
            if(result =="Product Not Found")
            {
                return NotFound(new { Message = "Product not found" });
            }
            else
            {
                return Ok(new { Message = result });
            }
        }
    }
}
