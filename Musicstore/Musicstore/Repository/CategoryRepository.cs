using Musicstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Musicstore.Repository {
    public class CategoryRepository : Repository<Category, string>, ICategoryRepository {
        public override Task<List<Category>> GetAllAsync() {
            return Task.FromResult((from c in DbContext.Categories select c).ToList());
        }

        public override Task<List<Category>> GetAsync(int ammount = 1) {
            if(ammount < 1) {
                throw new IndexOutOfRangeException();
            }
            return Task.FromResult((from c in DbContext.Categories.Take(ammount) select c).ToList());
        }

        public override Task<Category> GetByIdAsync(string id) {
            if(string.IsNullOrEmpty(id)) {
                throw new NullReferenceException();
            }
            return Task.FromResult((from c in DbContext.Categories where c.Id.Equals(id) select c).FirstOrDefault());
        }
    }
}