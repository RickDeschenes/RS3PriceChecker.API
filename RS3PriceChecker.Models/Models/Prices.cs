using RS3PriceChecker.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS3PriceChecker.Models
{
    public class Prices : BaseModel
    {
        [Required]
        public int ItemID { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
