using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Musicstore.Models {
    public class Song {
        public string Id { get; set; }
        [Display(Name = "Title")]
        public string Name { get; set; }
        public string Length { get; set; }
        public string Thumbnail { get; set; }
        public string Link { get; set; }
        [Display(Name = "Date created")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Date modified")]
        [DisplayFormat(NullDisplayText = "No modifications")]
        public DateTime? DateModified { get; set; }
        [ForeignKey("Album")]
        public string AlbumId { get; set; }
        [Required]
        [ForeignKey("Category")]
        [Display(Name = "Category")]
        public string CategoryId { get; set; }
        [Required]
        [ForeignKey("Performer")]
        [Display(Name = "Performer")]
        public string PerformerId { get; set; }
        public virtual Performer Performer { get; set; }
        public virtual Album Album { get; set; }
        public virtual Category Category { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [NotMapped]
        public IEnumerable<Category> Categories { get; set; }
        [NotMapped]
        public IEnumerable<Performer> Performers { get; set; }
    }
}