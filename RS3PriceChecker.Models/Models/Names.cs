using RS3PriceChecker.Models.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RS3PriceChecker.Models
{
    public class Names : BaseModel
    {
        [Required]
        public string Value { get; set; }
        [Required]
        public string Description { get; set; }

        public IEnumerable<Items> Items { get; set; }
    }
}
