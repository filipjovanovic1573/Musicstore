using Musicstore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Musicstore.Controllers {
    public class HomeController : BaseController {
        public ActionResult Index() {
            CategoryViewModel[] c = { new CategoryViewModel() { Name = "Rock", ImageLink = "http://i.imgur.com/XO3DZVV.jpg" },
                                      new CategoryViewModel() { Name = "Hip-Hop", ImageLink = "http://i.imgur.com/RYaSSs2.jpg"},
                                      new CategoryViewModel() { Name = "Pop", ImageLink = "http://i.imgur.com/kvEyari.jpg" },
                                      new CategoryViewModel() { Name = "Dubstep", ImageLink = "http://i.imgur.com/28jeHKi.jpg" },
                                      new CategoryViewModel() { Name = "House", ImageLink = "http://i.imgur.com/SfE206z.jpg" },
                                      new CategoryViewModel() { Name = "Jazz", ImageLink = "http://i.imgur.com/EWVevBD.jpg" } };
            ViewBag.Categories = c;
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PartnerProgram() {
            return View();
        }

        public ActionResult PartnerList() {
            return View();
        }
    }
}