using BuisnessLayer.Model;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.IRepository
{
    public interface IOrderRepo
    {
        Task PlaceOrderAsync(OrderModel order);
        Task<List<OrderModel>> GetAllOrdersAsync(int userId);
        List<int> GetTop5Orders(string month, int year);
    }
}
