using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specfications;
using Talabat.Repository.Data.Contexts;

namespace Talabat.Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreContext _dbContext;
        public GenericRepository(StoreContext dbContext) // Ask CLR for Creating Object From StoreContext Implicitly
        {
            _dbContext = dbContext;
        }

        #region Without Spec
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity) == typeof(Product))
                return (IEnumerable<TEntity>)await _dbContext.Products.Include(P => P.Brand).Include(P => P.Category).ToListAsync();


            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {

            if (typeof(TEntity) == typeof(Product))
                return await _dbContext.Products.Include(P => P.Brand).Include(P => P.Category).FirstOrDefaultAsync(P => P.Id == id as int?) as TEntity;
            return await _dbContext.Set<TEntity>().FindAsync(id);


        }

        #endregion

        #region With Spec 
        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity, TKey> Spec)
        {
            return await ApplySpecification(Spec).ToListAsync();
        }

        public async Task<TEntity> GetByIdWithSpecAsync(ISpecification<TEntity, TKey> Spec)
        {
            return await ApplySpecification(Spec).FirstOrDefaultAsync();
        }

        #endregion

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity,TKey> Spec)
        {
            return SpecificationEvalutor<TEntity, TKey>.GetQuery(_dbContext.Set<TEntity>(), Spec);
        }
        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
           _dbContext.Update(entity);
        }
        public void Delete(TEntity entity)
        {
         _dbContext.Remove(entity);
        }

     
    }
}
