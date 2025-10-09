using RS3PriceChecker.Entities;
using RS3PriceChecker.Models;
using System.Collections.Generic;

namespace RS3PriceChecker.Mapper
{
    public static class IconMapper
    {
        public static IconEntity ModelToEntity(Icons m)
        {
            return new IconEntity()
            {
                ID = m.ID,
                Small = m.Small,
                Large = m.Large,
                CreatedBy = m.CreatedBy,
                CreatedDate = m.CreatedDate,
                ModifiedBy = m.ModifiedBy,
                ModifiedDate = m.ModifiedDate
            };
        }

        public static List<IconEntity> ModelToEntityList(List<Icons> ms)
        {
            List<IconEntity> results = new();

            foreach (var m in ms)
            {
                results.Add(ModelToEntity(m));
            }
            return results;
        }

        public static Icons EntityToModel(IconEntity e)
        {
            return new Icons()
            {
                ID = e.ID,
                Small = e.Small,
                Large = e.Large,
                CreatedBy = e.CreatedBy,
                CreatedDate = e.CreatedDate,
                ModifiedBy = e.ModifiedBy,
                ModifiedDate = e.ModifiedDate
            };
        }

        public static List<Icons> EntityToModelList(List<IconEntity> es)
        {
            List<Icons> results = new();

            foreach (var e in es)
            {
                results.Add(EntityToModel(e));
            }
            return results;
        }
    }
}
