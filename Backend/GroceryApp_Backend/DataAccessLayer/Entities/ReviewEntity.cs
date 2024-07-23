using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class ReviewEntity
    {
        [Key]
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
        public string UserName { get; set; }
        public int ProductId { get; set; }
        public string Date { get; set; }
    }
}
