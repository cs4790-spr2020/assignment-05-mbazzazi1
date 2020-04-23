using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlabberApp.DataStore.Adapters;
using BlabberApp.DataStore.Plugins;
using BlabberApp.Domain.Entities;
using BlabberApp.Services;

namespace BlabberApp.ServicesTest
{
    [TestClass]
    public class BlabServiceTest
    {
        private BlabServiceFactory _blabServiceFactory = new BlabServiceFactory();

        [TestMethod]
        public void TestCanary()
        {
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void GetAllEmptyTest()
        {
            //Arrange
            BlabService blabService = _blabServiceFactory.CreateBlabService();
            ArrayList expected = new ArrayList();
            //Act
            IEnumerable actual = blabService.GetAll();
            //Assert
            Assert.AreEqual(expected.Count, (actual as ArrayList).Count);
        }

        [TestMethod]
        public void AddNewBlabTest()
        {
            //Arrange
            string email = "user@example.com";
            string msg = "Prow scuttle parrel provost Sail ho shrouds spirits boom mizzenmast yardarm.";
            BlabService blabService = _blabServiceFactory.CreateBlabService();
            Blab blab = blabService.CreateBlab(msg, email);
            blabService.AddBlab(blab);
            //Act
            var actual = Assert.ThrowsException<NotImplementedException>(() => blabService.FindUserBlabs(email));
            //Assert
            Assert.AreEqual("FindUserBlabs", actual.Message);
        }
    }
}