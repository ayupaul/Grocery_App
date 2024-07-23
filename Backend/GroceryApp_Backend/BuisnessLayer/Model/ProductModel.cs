using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Model
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public int AvailableQuanity { get; set; }
        public string ImageLink { get; set; }
        public int Price { get; set; }
        public string Specification { get; set; }
    }
}
