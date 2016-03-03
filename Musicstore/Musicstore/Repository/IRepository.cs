using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicstore.Repository {
    interface IRepository<TEntity, in TKey> where TEntity : class {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TKey id);
        Task<int> CreateAsync(TEntity model);
        Task<int> UpdateAsync(TEntity model);
        Task<List<TEntity>> GetAsync(int ammount = 1);
    }
}
