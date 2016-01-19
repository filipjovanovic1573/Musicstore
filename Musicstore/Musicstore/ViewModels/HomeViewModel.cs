using Musicstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Musicstore.ViewModels {
    public class HomeViewModel {
        public List<Category> Categories { get; set; }
        public List<Performer> Performers { get; set; }
        public List<Album> Albums { get; set; }
    }
}