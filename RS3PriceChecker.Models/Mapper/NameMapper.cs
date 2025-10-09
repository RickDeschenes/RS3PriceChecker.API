using RS3PriceChecker.Entities;
using RS3PriceChecker.Models;
using System.Collections.Generic;

namespace RS3PriceChecker.Mapper
{
    public static class NameMapper
    {
        public static NameEntity ModelToEntity(Names m)
        {
            return new NameEntity()
            {
                ID = m.ID,
                Value = m.Value,
                Description = m.Description,
                CreatedBy = m.CreatedBy,
                CreatedDate = m.CreatedDate,
                ModifiedBy = m.ModifiedBy,
                ModifiedDate = m.ModifiedDate
            };
        }

        public static List<NameEntity> ModelToEntityList(List<Names> ms)
        {
            List<NameEntity> results = new();

            foreach (var m in ms)
            {
                results.Add(ModelToEntity(m));
            }
            return results;
        }

        public static Names EntityToModel(NameEntity e)
        {
            return new Names()
            {
                ID = e.ID,
                Value = e.Value,
                Description = e.Description,
                CreatedBy = e.CreatedBy,
                CreatedDate = e.CreatedDate,
                ModifiedBy = e.ModifiedBy,
                ModifiedDate = e.ModifiedDate
            };
        }

        public static List<Names> EntityToModelList(List<NameEntity> es)
        {
            List<Names> results = new();

            foreach (var e in es)
            {
                results.Add(EntityToModel(e));
            }
            return results;
        }
    }
}
