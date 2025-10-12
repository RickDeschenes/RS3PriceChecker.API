using System;
using System.Collections.Generic;

namespace RS3PriceChecker.Database;

public partial class Icon
{
    public int Id { get; set; }

    public string Small { get; set; } = null!;

    public string Large { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
