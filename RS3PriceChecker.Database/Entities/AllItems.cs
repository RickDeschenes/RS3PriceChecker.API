using RS3PriceChecker.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS3PriceChecker.Database;

public class AllItems
{
    public int Id { get; set; }
    public int ItemID { get; set; } //"id": 50109,
    public string Examine { get; set; } = string.Empty; //"examine": "One of Howl's original invention blueprints that can be traded.",
    public bool Members { get; set; } //"members": true,
    public long LowAlch { get; set; } //"lowalch": 4000,
    public long Limit { get; set; } //"limit": 2,
    public long ValueTask { get; set; } //"value": 10000,
    public long HighAlch { get; set; } //"highalch": 6000,
    public Icon Icon { get; set; } = null!; //"icon": "'Ancient gizmos' blueprint.png",
    public Name Name { get; set; } = null!; //"name": "'Ancient gizmos' blueprint",
    long Price { get; set; } //"price": 5626574,
    long Prior { get; set; } //"last": 5626574,
    long Volume { get; set; } //"volume": 14
}
