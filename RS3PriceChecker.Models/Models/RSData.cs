using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS3PriceChecker.Models
{
    public class FilterValues
    {
        public List<string> Types { get; set; }
        public List<RSAlpha> Alpha { get; set; }
    }

    public class RSAlpha
    {
        public string Letter { get; set; }
        public int Items { get; set; }
    }

    public class RSRawItems
    {
        public int Total { get; set; }
        public List<RSItem> Items { get; set; }
    }

    public class RSItem
    {
        public string Icon { get; set; }
        public string Icon_Large { get; set; } 
        public int Id { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string TypeIcon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Members { get; set; }
        public List<RSPrice> Prices { get; set; }
    }
    public class RSPrice
    {
        public int Price { get; set; }
        public DateTime Date { get; set; }
    }
    public class RawPrice
    {
        public int Id { get; set; }
        public int Price { get; set; }
    }
}
