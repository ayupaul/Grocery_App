using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Model
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public string OrderDate { get; set; }
    }
}
