using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specfications
{
   public class BaseSpecifications<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; set; } = null; // P=>P.id==1
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>> (); //  P=>P.Brand , P=>P.Category
        public Expression<Func<TEntity, object>> OrderBy { get; set; }
        public Expression<Func<TEntity, object>> OrderByDesc { get; set; }
        public int Skip { get ; set; }
        public int Take { get ; set; }
        public bool IsPaginationEnabled { get; set; }


        // Get All
        public BaseSpecifications()
        {
           // Includes = new List<Expression<Func<T, object>>>();
        }

        // Get By Id

        public BaseSpecifications(Expression<Func<TEntity, bool>> Expression)
        {
            Criteria = Expression;
        }

        public void AddOrderBy(Expression<Func<TEntity, object>> expression) 
        {
            OrderBy = expression;
        }

        public void AddOrderByDesc(Expression<Func<TEntity, object>> expression)
        {
            OrderByDesc = expression;
        }

        public void ApplyPagination(int skip , int take) 
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take= take;
        }
    }
}
