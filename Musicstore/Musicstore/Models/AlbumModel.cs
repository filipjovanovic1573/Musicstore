using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Musicstore.Models {
    public class Album {
        [Key]
        public string Id { get; set; }
        [Required(ErrorMessage = "Album name is required")]
        [Display(Name = "Album name")]
        public string AlbumName { get; set; }
        [Display(Name = "# Songs")]
        public int NumberOfSongs { get; set; }
        public string Thumbnail { get; set; }
        [Required(ErrorMessage = "Year of production is required")]
        [Display(Name = "Year of production")]
        public DateTime Year { get; set; }
        [Display(Name = "Date created")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Date modified")]
        [DisplayFormat(NullDisplayText = "No modifications")]
        public DateTime DateModified { get; set; }
        [ForeignKey("Publisher")]
        public string PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        [ForeignKey("Performer")]
        public string PerformerId { get; set; }
        public virtual Performer Performer { get; set; }
        public virtual IEnumerable<Song> Songs { get; set; }

    }
}