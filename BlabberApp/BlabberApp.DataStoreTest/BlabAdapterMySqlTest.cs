using System.Collections;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlabberApp.DataStore.Adapters;
using BlabberApp.DataStore.Plugins;
using BlabberApp.Domain.Entities;

namespace BlabberApp.DataStoreTest
{
    [TestClass]
    public class BlabAdapter_MySql_UnitTests
    {
        private BlabAdapter _harness;

        [TestInitialize]
        public void Setup()
        {
            _harness = new BlabAdapter(new MySqlBlab());
            _harness.RemoveAll();
        }
        [TestCleanup]
        public void TearDown()
        {
            _harness.RemoveAll();
        }

        [TestMethod]
        public void Canary()
        {
            Assert.AreEqual(true, true);
        }

 [TestMethod]
        public void TestAddAndUpdateBlab() {
            //Arrange
            string email = "fooabar@example.com";
            User mockUser = new User(email);
            Blab blab = new Blab("Now is the time for, blabs...", mockUser);
            //Act
            _harness.Add(blab);
            blab.Message = "";
            _harness.Update(blab);
            var actual = _harness.GetById(blab.Id);
            //Assert
            Assert.AreEqual("", actual.Message);
        }

        [TestMethod]
        public void TestReadAll() {
            //Arrange
            string email = "fooabar@example.com";
            User mockUser = new User(email);
            Blab blab = new Blab("Now is the time for, blabs...", mockUser);
            //Act
            _harness.Add(blab);
            ArrayList actual = (ArrayList)_harness.GetAll();
            //Assert
            if (actual.Count > 0) {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "There is no row at position 0.")]
        public void TestAddAndRemoveAll() {
            //Arrange
            string email = "fooabar@example.com";
            User mockUser = new User(email);
            Blab blab = new Blab("Now is the time for, blabs...", mockUser);
            //Act
            _harness.Add(blab);
            _harness.Remove(blab);
            _harness.RemoveAll();
            var actual = _harness.GetById(blab.Id);
        }

        [TestMethod]
        public void TestAddAndGetBlab()
        {
            //Arrange
            string email = "fooabar@example.com";
            User mockUser = new User(email);
            Blab blab = new Blab("Now is the time for, blabs...", mockUser);
            //Act
            _harness.Add(blab);
            ArrayList actual = (ArrayList)_harness.GetByUserId(email);
            //Assert
            Assert.AreEqual(1, actual.Count);
        }
    }
}