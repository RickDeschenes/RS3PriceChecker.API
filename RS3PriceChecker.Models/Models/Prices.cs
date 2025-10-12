
namespace RS3PriceChecker.Models;

public class Prices
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public int Amount { get; set; }
    public DateTime Date { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
    public DateTime ModifiedDate { get; set; }
}
