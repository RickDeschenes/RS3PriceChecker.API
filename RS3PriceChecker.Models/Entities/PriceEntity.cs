using RS3PriceChecker.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS3PriceChecker.Entities
{
    public class PriceEntity : BaseEntity
    {
        public int ItemID { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
