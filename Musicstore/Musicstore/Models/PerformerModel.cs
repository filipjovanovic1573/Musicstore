using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Musicstore.Models {
    public class Performer {
        [Key]
        public string Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        [Display(Name = "# of albums")]
        public int NumberOfAlbums { get; set; }
        [Display(Name = "Date created")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Date modified")]
        [DisplayFormat(NullDisplayText = "No modifications")]
        public DateTime? DateModified { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

    }
}