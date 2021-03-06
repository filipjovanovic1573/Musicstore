﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Musicstore.Models {
    public class Partner {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool Approved { get; set; }
        [Display(Name = "Date created")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Date modified")]
        [DisplayFormat(NullDisplayText = "No modifications")]
        public DateTime? DateModified { get; set; }
    }
}