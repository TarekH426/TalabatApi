using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specfications;

namespace Talabat.Core.Repositories.Contract
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        #region Without Specifications
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TKey id);
        #endregion


        #region WithSpecifications
        Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity, TKey> Spec);
        Task<TEntity> GetByIdWithSpecAsync(ISpecification<TEntity, TKey> Spec);

        #endregion
        Task AddAsync(TEntity entity);
       void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
