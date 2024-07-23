using BuisnessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.IRepository
{
    public interface IUserRepo
    {
        Task<UserModel> AuthenticateAsync(UserModel user);
        Task<string> RegisterUserAsync(UserModel user);
        Task<List<UserModel>> GetAllUserAsync();
        Task<string> MakeAdminAsync(UserModel user);
      
    }
}
