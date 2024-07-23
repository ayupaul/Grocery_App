using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Model
{
    public class ReviewModel
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
        public string UserName { get; set; }
        public int ProductId { get; set; }
        public string Date { get; set; }
    }
}
