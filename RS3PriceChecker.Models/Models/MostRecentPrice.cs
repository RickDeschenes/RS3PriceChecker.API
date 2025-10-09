using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS3PriceChecker.Models
{
    public class MostRecentPrices
    {
        [Required]
        public int IID { get; set; }
        [Required]
        public int ItemID { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
