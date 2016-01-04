using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Musicstore.Models {
    public class Performer {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        [DisplayFormat(NullDisplayText = "No modifications")]
        public DateTime DateModified { get; set; }
    }
}