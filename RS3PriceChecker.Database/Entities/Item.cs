using System;
using System.Collections.Generic;

namespace RS3PriceChecker.Database;

public partial class Item
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public int CategoryId { get; set; }
    public int NameId { get; set; }
    public int IconId { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
    public DateTime ModifiedDate { get; set; }

    public virtual Category Category { get; set; } = null!;
    public virtual Icon Icon { get; set; } = null!;
    public virtual Name Name { get; set; } = null!;
    public virtual ICollection<Price> Prices { get; set; } = [];
}
