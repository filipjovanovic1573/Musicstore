using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Musicstore;
using Musicstore.Controllers;
using System.Threading.Tasks;

namespace Musicstore.Tests.Controllers {
    [TestClass]
    public class HomeControllerTest {
        [TestMethod]
        public void Index() {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index().Result as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About() {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact() {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AllCategories() {
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.AllCategories().Result as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AllPerformers() {
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.AllPerformers().Result as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AllAlbums() {
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.AllAlbums().Result as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AllPublishers() {
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.AllPublishers().Result as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AllPartners() {
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.AllPartners().Result as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
