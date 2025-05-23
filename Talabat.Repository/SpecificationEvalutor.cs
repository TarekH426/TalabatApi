using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specfications;

namespace Talabat.Repository
{
    public static class SpecificationEvalutor<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        // Functon to Build Query

        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery ,ISpecification<TEntity, TKey> Spec) 
        {
            var Query = inputQuery; // _dbContext.Set<T>()

            if (Spec.Criteria is not null) // P=> P.Id == Id
            {
               Query = Query.Where(Spec.Criteria); // _dbContext.Set<T>().where(P=> P.Id == Id)
            }
             
            if(Spec.OrderBy is not null)
            {
                Query= Query.OrderBy(Spec.OrderBy);
            }

            if (Spec.OrderByDesc is not null)
            {
                Query = Query.OrderByDescending(Spec.OrderByDesc);
            }

            // P=> P.ProductBrand , P=>P.ProductType

            Query = Spec.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));

            // _dbContext.Set<T>().where(P=>P.Id == Id).Include(P=>P.ProductBrand)
            // _dbContext.Set<T>().where(P=>P.Id == Id).Include(P=>P.ProductBrand).Include(P=>P.ProductType)

            if(Spec.IsPaginationEnabled)
            {
                Query=Query.Skip(Spec.Skip).Take(Spec.Take);
            }

            return Query;
        }
    }
}
