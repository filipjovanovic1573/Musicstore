using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Musicstore.Models;

namespace Musicstore.Repository {
    public class PartnerRepository : Repository<Partner, string>, IPartnerRepository {
        public override Task<List<Partner>> GetAllAsync() {
            return Task.FromResult((from p in DbContext.Partners select p).ToList());
        }

        public override Task<List<Partner>> GetAsync(int ammount = 1) {
            if(ammount < 1) {
                throw new IndexOutOfRangeException();
            }
            return Task.FromResult((from p in DbContext.Partners.Take(ammount) select p).ToList());
        }

        public override Task<Partner> GetByIdAsync(string id) {
            if(string.IsNullOrEmpty(id)) {
                throw new NullReferenceException();
            }
            return Task.FromResult((from p in DbContext.Partners where p.Id.Equals(id) select p).FirstOrDefault());
        }
    }
}