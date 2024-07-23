using AutoMapper;
using BuisnessLayer.IRepository;
using BuisnessLayer.Model;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Repository
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public ProductRepo(AppDbContext appDbContext,IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
        public async Task AddProductAsync(ProductModel product)
        {
            var productEntity=mapper.Map<ProductEntity>(product);
            await appDbContext.Products.AddAsync(productEntity);
            await appDbContext.SaveChangesAsync();
        }

        public async Task<string> DeleteProductAsync(int id)
        {
            var product = await appDbContext.Products.FindAsync(id);
            if (product == null)
            {
                return "Product Not Found";
            }
            appDbContext.Products.Remove(product);
            await appDbContext.SaveChangesAsync();
            return "Deleted Successfully!!!";
        }

        public async Task<List<ProductModel>> GetAllProductAsync()
        {
            var product = await appDbContext.Products.ToListAsync();
            var productModel=mapper.Map<List<ProductModel>>(product);
            return productModel;
        }

        public async Task<ProductModel> GetProductByIdAsync(int id)
        {
            var product = await appDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            var productModel= mapper.Map<ProductModel>(product);
            return productModel;
        }

        public async Task<List<ProductModel>> GetProductByNameAsync(string productName)
        {
            var productList = await appDbContext.Products.Where(x => x.ProductName == productName).ToListAsync();
            var productListModel=mapper.Map<List<ProductModel>>(productList);
            return productListModel;
        }

        public async Task<string> UpdateProductAsync(int id, ProductModel product)
        {
            var productDetails = await appDbContext.Products.FindAsync(id);
            if (productDetails == null)
            {
                return "Product Not Found";
            }
            productDetails.ProductName = product.ProductName;
            productDetails.Description = product.Description;
            productDetails.Category = product.Category;
            productDetails.Price = product.Price;
            productDetails.AvailableQuanity = product.AvailableQuanity;
            productDetails.ImageLink = product.ImageLink;
            productDetails.Specification = product.Specification;
            await appDbContext.SaveChangesAsync();
            return "Product Updated Successfully!!";
        }
    }
}
