
namespace RS3PriceChecker.Models;

public class Prices : BaseModel
{
    public int ItemID { get; set; }
    public int Amount { get; set; }
    public DateTime Date { get; set; }
}
