using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Musicstore.Models {
    public class Song {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Length { get; set; }
        public DateTime Year { get; set; }
        public string Genre { get; set; }
    }
}