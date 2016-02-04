using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Musicstore.Models {
    public class Api {
        [Key]
        public string Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [Display(Name = "Application name")]
        public string AppName { get; set; }
        public string Key { get; set; }
        public virtual User User { get; set; }
    }
}