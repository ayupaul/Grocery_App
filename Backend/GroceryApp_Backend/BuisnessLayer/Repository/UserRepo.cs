using AutoMapper;
using BuisnessLayer.AutoMapper;
using BuisnessLayer.IRepository;
using BuisnessLayer.Model;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Repository
{
    public class UserRepo : IUserRepo
    {
      
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public UserRepo(AppDbContext appDbContext,IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }

        public async Task<UserModel> AuthenticateAsync(UserModel user)
        {
            var userObj = await appDbContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password);
            if(userObj == null)
            {
                return null;
            }
            userObj.Token = CreateJwt(userObj);
            var userModel = mapper.Map<UserModel>(userObj);
            return userModel;
        }

        public async Task<List<UserModel>> GetAllUserAsync()
        {
            var users = await appDbContext.Users.ToListAsync();
            var userModel = mapper.Map<List<UserModel>>(users);
            return userModel;

        }

        public async Task<string> MakeAdminAsync(UserModel user)
        {
            var userData = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (user == null)
            {
                return "User Not found";
            }
            userData.Role = user.Role;
            await appDbContext.SaveChangesAsync();
            return "Admin Updated Successfully";
        }

        public async Task<string> RegisterUserAsync(UserModel user)
        {
            //var mapper = MappingConfig.Configure();
            var userEntity=mapper.Map<UserEntity>(user);
            if (userEntity.Role == null)
            {
                userEntity.Role = "User";
            }
            if (appDbContext.Users.Where(x => x.Email == user.Email).Any())
            {
                return "SignUp Fails";
            }
            await appDbContext.AddAsync(userEntity);
            await appDbContext.SaveChangesAsync();
            return "Registration Successful";
           
        }
        private string CreateJwt(UserEntity user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysecret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
               
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name,user.FullName),
                 new Claim("UserId", user.Id.ToString())
            });
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var token=jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
