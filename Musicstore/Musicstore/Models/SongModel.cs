using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Musicstore.Models {
    public class Song {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Name { get; set; }
        [Required]
        public string Length { get; set; }
        [Required]
        [Display(Name = "Year of production")]
        public DateTime Year { get; set; }
        [Required]
        [Display(Name = "Genre")]
        public string Genre { get; set; }
        [Required]
        public string Link { get; set; }

        [ForeignKey("Publisher")]
        public string PublisherId { get; set; }
        [ForeignKey("Performer")]
        public string PerformerId { get; set; }

        public virtual Publisher Publisher { get; set; }
        public virtual Performer Performer { get; set; }
    }
}