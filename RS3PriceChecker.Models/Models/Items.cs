
namespace RS3PriceChecker.Models
{
    public class Items : BaseModel
    {
        public int ItemID { get; set; }
        public int CategoryId { get; set; }
        public Categories Category { get; set; } = new Categories();

        public int NameId { get; set; }
        public Names Name { get; set; } = new();
        public int IconId { get; set; }
        public Icons Icon { get; set; } = new Icons();

    }
}
