using RS3PriceChecker.Models.Common;

namespace RS3PriceChecker.Entities
{
    public class ItemEntity : BaseEntity
    {
        public int ItemID { get; set; }
        public int CategoryId { get; set; }
        public int NameId { get; set; }
        public int IconId { get; set; }

    }
}
