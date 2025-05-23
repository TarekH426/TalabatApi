using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Core
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();

        // Create Repository<T> and Return

        IGenericRepository<TEntity,TKey> Repository<TEntity,TKey> () where TEntity : BaseEntity<TKey>;
    }
}
