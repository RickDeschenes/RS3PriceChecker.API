using RS3PriceChecker.Models.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RS3PriceChecker.Models
{
    public class Icons : BaseModel
    {
        [Required]
        public string Small { get; set; }
        [Required]
        public string Large { get; set; }

        public IEnumerable<Items> Items { get; set; }
    }
}
