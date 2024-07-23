using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CartEntity> Carts { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<ReviewEntity> Reviews { get; set; }
    }
}
