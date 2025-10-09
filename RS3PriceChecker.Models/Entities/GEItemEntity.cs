using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS3PriceChecker.Entities
{
    public class GEItemEntity
    {
        public int ItemID { get; set; }
        public Item Catagory { get; set; }
        public Item Item { get; set; }
        public List<Price> Prices { get; set; }
    }

    public class Price
    {
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }

    public class Item
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int ItemID { get; set; }
    }
}
