using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Musicstore.Other;

namespace Musicstore.Tests.Other {
    [TestClass]
    public class ApiKeyTest {
        [TestMethod]
        public void GetKey() {
            Assert.IsNotNull(ApiKey.GetKey());
        }
    }
}
