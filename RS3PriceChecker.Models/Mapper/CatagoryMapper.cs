using RS3PriceChecker.Entities;
using RS3PriceChecker.Models;
using System.Collections.Generic;

namespace RS3PriceChecker.Mapper
{
    public static class CategoryMapper
    {
        public static CategoryEntity ModelToEntity(Categories m)
        {
            return new CategoryEntity()
            {
                ID = m.ID,
                Name = m.Name,
                CreatedBy = m.CreatedBy,
                CreatedDate = m.CreatedDate,
                ModifiedBy = m.ModifiedBy,
                ModifiedDate = m.ModifiedDate
            };
        }

        public static List<CategoryEntity> ModelToEntityList(List<Categories> ms)
        {
            List<CategoryEntity> results = new();

            foreach (var m in ms)
            {
                results.Add(ModelToEntity(m));
            }
            return results;
        }

        public static Categories EntityToModel(CategoryEntity e)
        {
            return new Categories()
            {
                ID = e.ID,
                Name = e.Name,
                CreatedBy = e.CreatedBy,
                CreatedDate = e.CreatedDate,
                ModifiedBy = e.ModifiedBy,
                ModifiedDate = e.ModifiedDate
            };
        }

        public static List<Categories> EntityToModelList(List<CategoryEntity> es)
        {
            List<Categories> results = new();

            foreach (var e in es)
            {
                results.Add(EntityToModel(e));
            }
            return results;
        }
    }
}
