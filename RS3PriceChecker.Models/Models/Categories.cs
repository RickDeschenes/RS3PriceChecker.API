using RS3PriceChecker.Models.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RS3PriceChecker.Models
{
    public class Categories : BaseModel
    {
        [Required]
        public string Name { get; set; }

        public IEnumerable<Items> Items { get; set; }

    }
}
