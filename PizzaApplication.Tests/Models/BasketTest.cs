using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaApplication.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;

namespace PizzaApplication.Tests
{
    [TestClass]
    public class BasketTest
    {
        PizzaStoreContext context = new PizzaStoreContext();

        [TestInitialize]
        public void setUp()
        {
        }

        [TestCleanup]
        public void cleanUp()
        {
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
