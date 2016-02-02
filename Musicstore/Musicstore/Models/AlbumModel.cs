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
        [DisplayFormat(NullDisplayText = "No album")]
        [Display(Name = "Album name")]
        public string AlbumName { get; set; }
        [Display(Name = "# Songs")]
        public int NumberOfSongs { get; set; }
        public string Thumbnail { get; set; }
        [Required(ErrorMessage = "Year of production is required")]
        [Display(Name = "Year of production")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Year { get; set; }
        [Display(Name = "Date created")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Date modified")]
        [DisplayFormat(NullDisplayText = "No modifications")]
        public DateTime? DateModified { get; set; }
        [ForeignKey("Publisher")]
        [Display(Name = "Publisher")]
        public string PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        [ForeignKey("Performer")]
        [Display(Name = "Performer")]
        public string PerformerId { get; set; }
        public virtual Performer Performer { get; set; }
        public virtual IEnumerable<Song> Songs { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [NotMapped]
        public IEnumerable<Song> SongsSL { get; set; }
        [NotMapped]
        public IEnumerable<Performer> Performers { get; set; }
        [NotMapped]
        public IEnumerable<Publisher> Publishers { get; set; }
    }
}