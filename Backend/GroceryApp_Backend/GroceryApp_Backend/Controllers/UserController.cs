using BuisnessLayer.AutoMapper;
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
    public class UserController : Controller
    {
       // private readonly AppDbContext appDbContext;
        private readonly IUserRepo userRepo;

        public UserController(/*AppDbContext appDbContext*/IUserRepo userRepo)
        {
            //this.appDbContext = appDbContext;
            this.userRepo = userRepo;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserModel user)
        {
            if(user == null)
            {
                return BadRequest();
            }
       
            //var userObj=await appDbContext.Users.FirstOrDefaultAsync(x=>x.Email==user.Email && x.Password==user.Password);
            var userObj = await userRepo.AuthenticateAsync(user);
           
            if (userObj == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Login Fails..." });

            }
            return Ok(new
            {
                Token= userObj.Token,
                Message = "Login Succeed"
            });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserModel user)
        {
            if (user == null)
            {
                return BadRequest();
            }
           
            var result=await userRepo.RegisterUserAsync(user);
            if (result == "SignUp Fails")
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Email Already exists" });
            }
            else
            {
                return Ok(new { Message = result });
            }
        }
        [HttpGet("getAllUser")]
        public async Task<IActionResult> GetAllUsers()
        {
            //var users = await appDbContext.Users.ToListAsync();
            var users = await userRepo.GetAllUserAsync();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
        [HttpPut("updateAdmin")]
        public async Task<IActionResult> UpdateAdmin(UserModel users)
        {
            //var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == users.Id);
            //if(user == null)
            //{
            //    return NotFound();
            //}
            //user.Role = users.Role;
            //await appDbContext.SaveChangesAsync();
            //return Ok(new { Message = "Role Updated Successfully" });
            var result=await userRepo.MakeAdminAsync(users);
            if(result=="User Not Found")
            {
                return NotFound();
            }
            else
            {
                return Ok(new { Message = result });
            }
        }
    }
}
