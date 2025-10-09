using RS3PriceChecker.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS3PriceChecker.Models
{
    public class Items : BaseModel
    {
        [Required]
        public int ItemID { get; set; }

        [ForeignKey("FK_Item_Category_ID")]
        public int CategoryId { get; set; }
        public Categories Category { get; set; }

        [ForeignKey("FK_Item_Name_ID")]
        public int NameId { get; set; }
        public Names Name { get; set; }

        [ForeignKey("FK_Item_Icon_ID")]
        public int IconId { get; set; }
        public Icons Icon { get; set; }

    }
}
