using Musicstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Musicstore.Repository {
    public class SongRepository : Repository<Song, string>, ISongRepository {
        public override Task<List<Song>> GetAllAsync() {
            return Task.FromResult((from s in DbContext.Songs select s).ToList());
        }

        public override Task<List<Song>> GetAsync(int ammount = 1) {
            if(ammount < 1) {
                throw new IndexOutOfRangeException();
            }
            return Task.FromResult((from s in DbContext.Songs.Take(ammount) select s).ToList());
        }

        public override Task<Song> GetByIdAsync(string id) {
            if(string.IsNullOrEmpty(id)) {
                throw new NullReferenceException();
            }
            return Task.FromResult((from s in DbContext.Songs where s.Id.Equals(id) select s).FirstOrDefault());
        }
    }
}