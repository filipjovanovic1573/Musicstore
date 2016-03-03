using Musicstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicstore.Repository {
    interface ISongRepository : IRepository<Song, string> {
    }
}
