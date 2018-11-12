using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CashRegister.Tests
{
    [TestClass]
    public class PredefinedTests
    {
        [TestMethod]
        public void PredifinedItems()
        {
            Predefined predefined = new Predefined();
            IEnumerable<ShoppingItem> items = predefined.GetAllItems();

            ShoppingItem bread = items.First(f => f.Code == "BRD");
            ShoppingItem apple = items.First(f => f.Code == "APL");
            ShoppingItem banana = items.First(f => f.Code == "BNA");

            Assert.AreEqual("Bread", bread.Name, "This item should a bread");
            Assert.AreEqual("Apple", apple.Name, "This item should an apple");
            Assert.AreEqual("Banana", banana.Name, "This item should a banana");
        }

        //[TestMethod]
        //public void PredifinedCoupons()
        //{
        //    Predefined predefined = new Predefined();
        //    IEnumerable<Coupon> coupons = predefined.GetAllCoupons();

        //    Coupon tenPercent = coupons.First(f => f.Code == "10Percent");
        //    Coupon fifteenPercent = coupons.First(f => f.Code == "15Percent");
        //    Coupon twoBananasOneAppleFree = coupons.First(f => f.Code == "Buy2BananasGetOneAppleFree");

        //    Assert.AreEqual("10Percent", tenPercent.Code, "This coupon code should be 10Percent");
        //    Assert.AreEqual("15Percent", fifteenPercent.Code, "This coupon code should be 15Percent");
        //    Assert.AreEqual("Buy2BananasGetOneAppleFree", twoBananasOneAppleFree.Code, "This coupon code should be Buy2BananasGetOneAppleFree");
        //}
    }
}
