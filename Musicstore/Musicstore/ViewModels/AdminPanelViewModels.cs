using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Musicstore.ViewModels {
    public class DetailsViewModel {
        public string Id { get; set; }
        [Display(Name = "Username")]
        public string UserName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Email confirmed")]
        public bool EmailConfirmed { get; set; }
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Phone confirmed")]
        public bool PhoneNumberConfirmed { get; set; }
        [Display(Name = "TwoFactor Auth")]
        public bool TwoFactor{ get; set; }
        [Display(Name = "Lockout")]
        public bool LockoutEnabled { get; set; }
        [Display(Name = "Locked until")]
        public DateTime? LockoutEndDate { get; set; }
        [Display(Name = "Roles")]
        public string[] Roles { get; set; }
    }

    public class EditViewModel {
        public string Id { get; set; }
        [Display(Name = "Username")]
        public string UserName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Roles")]
        public string[] Roles { get; set; }
    }
}