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
    public class CartRepo : ICartRepo
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public CartRepo(AppDbContext appDbContext,IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
        public async Task AddToCartAsync(CartModel model)
        {
            var cartEntity=mapper.Map<CartEntity>(model);
            await appDbContext.Carts.AddAsync(cartEntity);
            await appDbContext.SaveChangesAsync();
        }

        public async Task<CartModel> CheckProductInCartAsync(CartModel model)
        {
            var product = await appDbContext.Carts.FirstOrDefaultAsync(x => x.UserId == model.UserId && x.ProductId == model.ProductId);
            var cartModel=mapper.Map<CartModel>(product);
            return cartModel;
        }

        public async Task<IEnumerable<CartModel>> GetAllAsync()
        {
            var cartItems = await appDbContext.Carts.ToListAsync();
            var cartItemsModel=mapper.Map<List<CartModel>>(cartItems);
            return cartItemsModel;
        }

        public async Task<IEnumerable<CartModel>> GetItemsFromUserAsync(int userId)
        {
            var cartItems = await appDbContext.Carts.Where(x => x.UserId == userId).ToListAsync();
            var cartItemsModel = mapper.Map<List<CartModel>>(cartItems);
            return cartItemsModel;
        }

        public async Task<string> RemoveAllItemsFromCartAsync(int userId)
        {
            var cartItems = await appDbContext.Carts.Where(x => x.UserId == userId).ToListAsync();
            if (cartItems.Count == 0)
            {
                return "null";
            }
            appDbContext.Carts.RemoveRange(cartItems);
            await appDbContext.SaveChangesAsync();
            return "Removed Successfully";
        }

        public async Task<string> RemoveFromCartAsync(int cartId)
        {
            var cartItem = await appDbContext.Carts.FirstOrDefaultAsync(x => x.CartId == cartId);
            if (cartItem == null)
            {
                return "null";
            }
            appDbContext.Carts.Remove(cartItem);
            await appDbContext.SaveChangesAsync();
            return "";
        }

        public async Task<string> UpdateAvailableQuanityAsync(ProductModel productModel)
        {
            var product = await appDbContext.Products.FirstOrDefaultAsync(x => x.Id == productModel.Id); ;
            if (product == null)
            {
                return "null";
            }
            product.AvailableQuanity = productModel.AvailableQuanity;
            if (product.AvailableQuanity < 0)
            {
                product.AvailableQuanity = 0;
            }
            await appDbContext.SaveChangesAsync();
            return "Success";
        }

        public async Task<string> UpdateQuantityAsync(CartModel model)
        {
            var product = await appDbContext.Carts.FirstOrDefaultAsync(x => x.UserId == model.UserId && x.ProductId == model.ProductId);
            if (product == null)
            {
                return "null";
            }
            product.Quantity = model.Quantity;
            appDbContext.SaveChanges();
            return "Updated Quantity Successfully";
        }
    }
}
