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
    public class ReviewController : Controller
    {
        //private readonly AppDbContext appDbContext;
        private readonly IReviewRepo reviewRepo;

        public ReviewController(/*AppDbContext appDbContext*/IReviewRepo reviewRepo)
        {
            //this.appDbContext = appDbContext;
            this.reviewRepo = reviewRepo;
        }
        [HttpPost("addReview")]
        public async Task<IActionResult> AddReview(ReviewModel review)
        {
            if(review == null)
            {
                return BadRequest();
            }
            await reviewRepo.AddReviewAsync(review);
            //await appDbContext.Reviews.AddAsync(review);
            //await appDbContext.SaveChangesAsync();
            return Ok(new { Message = "Review Added Successfully" });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewByProductId([FromRoute] int id)
        {
            //var review=await appDbContext.Reviews.Where(x=>x.ProductId==id).ToListAsync();
            var review=await reviewRepo.GetAllReviewsByProductIdAsync(id);
            return Ok(review);
        }
    }
}
