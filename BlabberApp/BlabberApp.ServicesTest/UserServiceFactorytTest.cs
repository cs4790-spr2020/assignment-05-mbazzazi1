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
    public class UserServiceFactoryTest
    {
        UserServiceFactory harness = new UserServiceFactory();

        [TestMethod]
        public void CanaryTest()
        {
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void BuildAdapterPluginTest()
        {
            //Arrange and Act
            UserAdapter userAdapter = harness.CreateUserAdapter();
            //Assert
            Assert.IsTrue(userAdapter is UserAdapter);
        }
        [TestMethod]
        public void BuildServiceAdapterPluginTest()
        {
            //Arrange and Act
            UserService userService = harness.CreateUserService();
            //Assert
            Assert.IsTrue(userService is UserService);
        }
    }
}
