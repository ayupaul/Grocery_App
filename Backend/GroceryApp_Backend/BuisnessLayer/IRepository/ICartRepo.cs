using BuisnessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.IRepository
{
    public interface ICartRepo
    {
        Task AddToCartAsync(CartModel model);
        Task<IEnumerable<CartModel>> GetAllAsync();
        Task<IEnumerable<CartModel>> GetItemsFromUserAsync(int userId);
        Task<string> UpdateQuantityAsync(CartModel model);
        Task<CartModel> CheckProductInCartAsync(CartModel model);
        Task<string> RemoveAllItemsFromCartAsync(int userId);
        Task<string> UpdateAvailableQuanityAsync(ProductModel productModel);
        Task<string> RemoveFromCartAsync(int cartId);
    }
}
