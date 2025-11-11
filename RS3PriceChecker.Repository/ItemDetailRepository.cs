using AutoMapper;
using Microsoft.Extensions.Logging;
using RS3PriceChecker.Database;
using RS3PriceChecker.Models;

namespace RS3PriceChecker.Repository;

public class ItemDetailRepository(RS3DbContext context, IMapper mapper)
{
    protected readonly RS3DbContext db = context;

    private readonly IMapper _mapper = mapper;

    #region Compound Crud

    public List<MostRecentPrices> GetRecentPrices()
    {
        return _mapper.Map<List<MostRecentPrice>, List<MostRecentPrices>>([.. db.MostRecentPrice]);
    }

    public ItemDetails GetItemDetail(int itemID)
    {
        DateTime date = DateTime.Today.AddDays(-1);

        var item = db.Item.Where(w => w.ItemId == itemID).FirstOrDefault() ?? new();
        var i = db.Icon.Where(w => w.Id == item.IconId).FirstOrDefault() ?? new();
        var c = db.Category.Where(w => w.Id == item.CategoryId).FirstOrDefault() ?? new();
        var n = db.Name.Where(w => w.Id == item.NameId).FirstOrDefault() ?? new();

        var p = _mapper.Map<List<Price>, List<Prices>>([.. db.Price.Where(w => w.ItemId == item.Id)]);

        return new ItemDetails
        {
            Catagory = item.Category.Name,
            Date = item.ModifiedDate,
            ItemId = item.ItemId,
            LargeIcon = item.Icon.Large,
            Name = item.Name.Value,
            Description = item.Name.Description,
            Prices = p,
            SmallIcon = item.Icon.Small
        };
    }

    public ItemDetails CreateItemDetail(ItemDetails request)
    {
        ItemDetails results = new();

        string createBy = "RickD";
        DateTime date = DateTime.Now;

        var c = db.Category.Where(w => w.Name == request.Catagory).FirstOrDefault();
        if (c == null || c.Id <= 0)
            return results;

        var icon = _mapper.Map<Icon, Icons>(db.Icon.Where(w => w.Small == request.SmallIcon).FirstOrDefault() ?? new());
        if (icon == null || icon.Id <= 0)
            //we have a new item, store it            
            icon = CreateIcon(new Icons()
            {
                Small = request.SmallIcon,
                Large = request.LargeIcon,
                CreatedBy = createBy,
                CreatedDate = date,
                ModifiedBy = createBy,
                ModifiedDate = date
            });

        var name = _mapper.Map<Name, Names>(db.Name.Where(w => w.Value == request.Name).FirstOrDefault() ?? new());
        if (name == null || name.Id <= 0)
            name = CreateName(new Names()
            {
                Value = request.Name,
                Description = request.Description,
                CreatedBy = createBy,
                CreatedDate = date,
                ModifiedBy = createBy,
                ModifiedDate = date
            });

        var item = _mapper.Map<Item, Items>(db.Item.Where(w => w.ItemId == request.ItemId).FirstOrDefault() ?? new());
        if (item == null || item.Id <= 0)
            item = CreateItem(new Models.Items()
            {
                CategoryId = c.Id,
                IconId = icon.Id,
                ItemID = request.ItemId,
                NameId = name.Id,
                CreatedBy = createBy,
                CreatedDate = date,
                ModifiedBy = createBy,
                ModifiedDate = date
            });

        if (item != null && item.Id > 0)
            CreatePrices(request.Prices, item.Id, createBy, date);

        return GetItemDetail(item?.ItemID ?? 0);
    }

    public static ItemDetails UpdateItemDetail(ItemDetails request)
    {
        // Implementation for updating item details can be added here

        return request;

    }

    #endregion Compound Crud

    #region Crud Items

    public List<Prices> GetAllPricesByItemID(int itemId)
    {
        return _mapper.Map<List<Price>, List<Prices>>([.. db.Price.Where(w => w.ItemId == itemId)]);
    }

    public List<Prices> GetAllPrices()
    {
        return _mapper.Map<List<Price>, List<Prices>>([.. db.Price]);
    }

    public Prices CreatePrice(Prices price)
    {
        using var transaction = db.Database.BeginTransaction();
        db.Price.Add(_mapper.Map<Prices, Price>(price));
        db.SaveChanges();
        db.Database.CommitTransaction();
        return GetAllPricesByItemID(price.ItemId).Where(w => w.Date == price.Date).FirstOrDefault() ?? new Prices();
    }

    public List<Prices> CreatePrices(List<Prices>  rawprices, int iD, string createBy, DateTime date)
    {
        List<Prices> prices = [];

        foreach (var p in rawprices)
        {
            var item = db.Price.Where(w => w.ItemId == iD && w.Date == p.Date).FirstOrDefault();
            if (item == null || item.Id <= 0)
            {
                p.ItemId = iD;
                p.CreatedBy = createBy;
                p.CreatedDate = date;
                p.ModifiedBy = createBy;
                p.ModifiedDate = date;
                prices.Add(p);
            }
        }

        using var transaction = db.Database.BeginTransaction();
        db.Price.AddRange(_mapper.Map<List<Prices>, List<Price>>(prices));
        db.SaveChanges();
        db.Database.CommitTransaction();
        
        return prices;
    }

    public Prices UpdatePrice(Prices price)
    {
        using var transaction = db.Database.BeginTransaction();
        db.Price.Update(_mapper.Map<Prices, Price>(price));
        db.SaveChanges();
        db.Database.CommitTransaction();
        return price;
    }

    public List<Prices> UpdatePrices(List<Prices> prices)
    {
        using var transaction = db.Database.BeginTransaction();
        db.Price.UpdateRange(_mapper.Map<List<Prices>, List<Price>>(prices));
        db.SaveChanges();
        db.Database.CommitTransaction();
        return prices;
    }

    public Categories GetCatagoryByID(int ID)
    {
        return _mapper.Map <Category, Categories>(db.Category.Where(w => w.Id == ID).FirstOrDefault() ?? new());
    }

    public Categories GetCatagoryByName(string Name)
    {
        return _mapper.Map<Category, Categories>(db.Category.Where(w => w.Name == Name).FirstOrDefault() ?? new());
    }

    public List<Categories> GetCatagories()
    {
        return  _mapper.Map<List<Category>, List<Categories>>([.. db.Category.Where(w => !w.Name.StartsWith("Deleted - "))]);
    }

    public Categories CreateCatagory(Categories catagory)
    {
        using var transaction = db.Database.BeginTransaction();
        db.Category.Add(_mapper.Map<Categories, Category>(catagory));
        db.SaveChanges();
        db.Database.CommitTransaction();
        return GetCatagoryByName(catagory.Name);
    }

    public List<Categories> CreateCatagories(List<Categories> catagories)
    {
        using var transaction = db.Database.BeginTransaction();
        db.Category.AddRange(_mapper.Map<List<Categories>, List<Category>>(catagories));
        db.SaveChanges();
        db.Database.CommitTransaction();
        return catagories;
    }

    public Categories UpdateCatagory(Categories catagory)
    {
        using var transaction = db.Database.BeginTransaction();
        db.Category.UpdateRange([_mapper.Map<Categories, Category>(catagory)]);
        db.SaveChanges();
        db.Database.CommitTransaction();
        return catagory;
    }

    public List<Categories> UpdateCatagories(List<Categories> catagories)
    {
        using var transaction = db.Database.BeginTransaction();
        db.Category.UpdateRange(_mapper.Map<List<Categories>, List<Category>>(catagories));
        db.SaveChanges();
        db.Database.CommitTransaction();
        return catagories;
    }

    public Icons GetIcon(string small)
    {
        return _mapper.Map<Icon, Icons>(db.Icon.Where(w => w.Small == small).FirstOrDefault() ?? new Icon());
    }

    public Icons CreateIcon(Icons icon)
    {
        using var transaction = db.Database.BeginTransaction();
        db.Icon.Add(_mapper.Map<Icons, Icon>(icon));
        db.SaveChanges();
        db.Database.CommitTransaction();

        return GetIcon(icon.Small);
    }

    public List<Icons> CreateIcons(List<Icons> icons)
    {
        using var transaction = db.Database.BeginTransaction();
        db.Icon.AddRange(_mapper.Map<List<Icons>, List<Icon>>(icons));
        db.SaveChanges();
        db.Database.CommitTransaction();
        return icons;
    }

    public List<Icons> UpdateIcons(List<Icons> icons)
    {
        using var transaction = db.Database.BeginTransaction();
        db.Icon.UpdateRange(_mapper.Map<List<Icons>, List<Icon>>(icons));
        db.SaveChanges();
        db.Database.CommitTransaction();
        return icons;
    }

    public Items GetItem(int itemID)
    {
        return _mapper.Map<Item, Items>(db.Item.Where(w => w.ItemId == itemID).FirstOrDefault() ?? new());
    }

    public List<Items> GetAllItems()
    {
        return _mapper.Map<List<Item>, List<Items>>([.. db.Item]);
    }

    public Items CreateItem(Items item)
    {
        using var transaction = db.Database.BeginTransaction();
        db.Item.Add(_mapper.Map<Items, Item>(item));
        db.SaveChanges();
        db.Database.CommitTransaction();

        return GetItem(item.ItemID);
    }

    public List<Items> CreateItems(List<Items> items)
    {
        using var transaction = db.Database.BeginTransaction();
        db.Item.AddRange(_mapper.Map<List<Items>, List<Item>>(items));
        db.SaveChanges();
        db.Database.CommitTransaction();
        return items;
    }

    public List<Items> UpdateItems(List<Items> items)
    {
        using var transaction = db.Database.BeginTransaction();
        db.Item.UpdateRange(_mapper.Map<List<Items>, List<Item>>(items));
        db.SaveChanges();
        db.Database.CommitTransaction();
        return items;
    }

    public Names GetName(string value)
    {
        return _mapper.Map<Name, Names>(db.Name.Where(w => w.Value == value).FirstOrDefault() ?? new Name());
    }

    public Names CreateName(Names name)
    {
        using var transaction = db.Database.BeginTransaction();
        db.Name.Add(_mapper.Map<Names, Name>(name));
        db.SaveChanges();
        db.Database.CommitTransaction();

        return GetName(name.Value);
    }

    public List<Names> CreateNames(List<Names> names)
    {
        using var transaction = db.Database.BeginTransaction();
        db.Name.AddRange(_mapper.Map<List<Names>, List<Name>>(names));
        db.SaveChanges();
        db.Database.CommitTransaction();
        return names;
    }

    public List<Names > UpdateNames(List<Names> names)
    {
        using var transaction = db.Database.BeginTransaction();
        db.Name.UpdateRange(_mapper.Map<List<Names>, List<Name>>(names));
        db.SaveChanges();
        db.Database.CommitTransaction();
        return names;
    }

    #endregion

}
