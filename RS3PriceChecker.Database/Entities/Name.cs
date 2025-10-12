using System;
using System.Collections.Generic;

namespace RS3PriceChecker.Database;

public partial class Name
{
    public int Id { get; set; }
    public string Value { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<Item> Items { get; set; } = [];
}
