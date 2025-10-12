
namespace RS3PriceChecker.Models;

public class ItemDetails
{
    public int ID { get; set; }
    public int ItemID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Catagory { get; set; } = string.Empty;
    public string SmallIcon { get; set; } = string.Empty;
    public string LargeIcon { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public List<Prices> Prices { get; set; } = [];
}
