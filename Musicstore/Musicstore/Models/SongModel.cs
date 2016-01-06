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
        [Display(Name = "Genre")]
        public string Genre { get; set; }
        public string Thumbnail { get; set; }
        [Required]
        public string Link { get; set; }
        [ForeignKey("Album")]
        public string AlbumId { get; set; }
        public virtual Album Album { get; set; }
    }
}