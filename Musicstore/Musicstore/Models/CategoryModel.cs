using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Musicstore.Models {
    public class Category {
        [Key]
        public string Id { get; set; }
        [Display(Name = "Category")]
        public string Name { get; set; }
        public string Image { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}