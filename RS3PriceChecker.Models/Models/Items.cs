
namespace RS3PriceChecker.Models
{
    public class Items
    {
        public int Id { get; set; }
        public int ItemID { get; set; }
        public int CategoryId { get; set; }
        public int NameId { get; set; }
        public int IconId { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; } = string.Empty;
        public DateTime ModifiedDate { get; set; }
    }
}
