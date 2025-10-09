using RS3PriceChecker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS3PriceChecker.Entities
{
    public class ItemDetail
    {
        public int ID { get; set; }
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Catagory { get; set; }
        public string SmallIcon { get; set; }
        public string LargeIcon { get; set; }
        public DateTime Date { get; set; }
        public List<PriceEntity> Prices { get; set; }
    }

}
