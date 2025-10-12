
namespace RS3PriceChecker.Models;

public class FilterValues
{
    public List<string> Types { get; set; } = [];
    public List<RSAlpha> Alpha { get; set; } = [];
}

public class RSAlpha
{
    public string Letter { get; set; } = string.Empty;
    public int Items { get; set; }
}

public class RSRawItems
{
    public int Total { get; set; }
    public List<RSItem> Items { get; set; } = [];
}

public class RSItem
{
    public string Icon { get; set; } = string.Empty;
    public string Icon_Large { get; set; } = string.Empty;
    public int Id { get; set; }
    public string Category { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Type { get; set; } = string.Empty;
    public string TypeIcon { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Members { get; set; } = string.Empty;
    public List<RSPrice> Prices { get; set; } = [];
}
public class RSPrice
{
    public int Price { get; set; }
    public DateTime Date { get; set; }
}
public class RawPrice
{
    public int Id { get; set; }
    public int Price { get; set; }
}
