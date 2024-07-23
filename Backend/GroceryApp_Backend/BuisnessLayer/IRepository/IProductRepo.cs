using BuisnessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.IRepository
{
    public interface IProductRepo
    {
        Task AddProductAsync(ProductModel product);
        Task<List<ProductModel>> GetProductByNameAsync(string productName);
        Task<List<ProductModel>> GetAllProductAsync();
        Task<ProductModel> GetProductByIdAsync(int id);
        Task<string> UpdateProductAsync(int id,ProductModel product);
        Task<string> DeleteProductAsync(int id);
    }
}
