using System;
using System.Collections.Generic;

namespace RS3PriceChecker.Database;

public partial class Price
{
    public int Id { get; set; }

    public int ItemId { get; set; }

    public int Amount { get; set; }

    public DateTime Date { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public virtual Item Item { get; set; } = null!;
}
