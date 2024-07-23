using AutoMapper;
using BuisnessLayer.Model;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.AutoMapper
{
    public class MappingConfig
    {
        public static IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserEntity, UserModel>();
                cfg.CreateMap<UserModel,UserEntity>();
                cfg.CreateMap<ProductModel,ProductEntity>();
                cfg.CreateMap<ProductEntity,ProductModel>();
                cfg.CreateMap<CartEntity,CartModel>();
                cfg.CreateMap<CartModel,CartEntity>();
                cfg.CreateMap<OrderModel,OrderEntity>();
                cfg.CreateMap<OrderEntity,OrderModel>();
                cfg.CreateMap<ReviewModel,ReviewEntity>();
                cfg.CreateMap<ReviewEntity,ReviewModel>();
                // Add more mapping configurations as needed
            });

            return config.CreateMapper();
        }
    }
}
