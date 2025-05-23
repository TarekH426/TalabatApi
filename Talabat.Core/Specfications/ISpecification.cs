using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specfications
{
    public interface ISpecification<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        // Sign for Property for where Condition [where(P=>P.Id == Id)]
        public Expression<Func<TEntity, bool>> Criteria { get; set; }

        // Sign for property for list of Include [Include(P=>P.productBrand).Include(P=>P.ProductType)]

        public List<Expression<Func<TEntity, object>>> Includes { get; set; }

        public Expression<Func<TEntity,object>> OrderBy { get; set; }
        public Expression<Func<TEntity,object>> OrderByDesc { get; set; }

        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }
    }
}
