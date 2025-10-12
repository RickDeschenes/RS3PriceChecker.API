using System;
using System.Collections.Generic;

namespace RS3PriceChecker.Database;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Icon { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
