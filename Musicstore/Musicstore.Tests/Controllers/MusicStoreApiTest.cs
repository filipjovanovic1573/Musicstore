using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Musicstore.Controllers;
using System.Web.Mvc;
using System.Web.Http;

namespace Musicstore.Tests.Controllers {
    [TestClass]
    public class MusicStoreApiTest {
        [TestMethod]
        public void GetSongs() {
            MusicStoreApiController api = new MusicStoreApiController();
            IHttpActionResult result = api.GetSongs("x3vkcdlwf2u").Result as IHttpActionResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAlbums() {
            MusicStoreApiController api = new MusicStoreApiController();
            IHttpActionResult result = api.GetAlbums("x3vkcdlwf2u").Result as IHttpActionResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetPerformers() {
            MusicStoreApiController api = new MusicStoreApiController();
            IHttpActionResult result = api.GetPerformers("x3vkcdlwf2u").Result as IHttpActionResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCategories() {
            MusicStoreApiController api = new MusicStoreApiController();

            IHttpActionResult result = api.GetCategories("x3vkcdlwf2u").Result as IHttpActionResult;

            Assert.IsNotNull(result);

        }
    }
}
