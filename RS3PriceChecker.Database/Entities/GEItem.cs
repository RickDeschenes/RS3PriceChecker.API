
namespace RS3PriceChecker.Database;

public class GEItem
{
    public int ItemID { get; set; }
    public Items Catagorys { get; set; } = new();
    public Items Items { get; set; } = new();
    public List<Prices> Prices { get; set; } = [];
}

public class Prices
{
    public int Amount { get; set; }
    public DateTime Date { get; set; }
}

public class Items
{
    public string Description { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public int ItemID { get; set; }
}
