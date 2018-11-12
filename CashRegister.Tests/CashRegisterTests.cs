using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using CashRegister.Coupons;

namespace CashRegister.Tests
{
    [TestClass]
    public class CashRegisterTests
    {
        private CashRegister _cashRegister;
        private IEnumerable<ShoppingItem> _itemsInStore;

        [TestInitialize]
        public void Initialize()
        {
            _cashRegister = new CashRegister();
            _itemsInStore = _cashRegister.GetAvailabeItemsInStore();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _cashRegister = null;
            _itemsInStore = null;
        }

        [TestMethod]
        public void InitilizeCashRegister()
        {            
            var couponsInStore = _cashRegister.GetAvailabeCouponsInStore();

            AssertItemsInStore(_itemsInStore);

            AssertCouponsInStore(couponsInStore);
        }

        private static void AssertCouponsInStore(IEnumerable<ICoupon> couponsInStore)
        {
            ICoupon tenPercent = couponsInStore.First(f => f.Code == "10Percent");
            ICoupon fifteenPercent = couponsInStore.First(f => f.Code == "15Percent");

            Assert.AreEqual("10Percent", tenPercent.Code, "This coupon code should be 10Percent");
            Assert.AreEqual("15Percent", fifteenPercent.Code, "This coupon code should be 15Percent");
        }

        private static void AssertItemsInStore(IEnumerable<ShoppingItem> itemsInStore)
        {
            ShoppingItem bread = itemsInStore.First(f => f.Code == "BRD");
            ShoppingItem apple = itemsInStore.First(f => f.Code == "APL");
            ShoppingItem banana = itemsInStore.First(f => f.Code == "BNA");

            Assert.AreEqual("Bread", bread.Name, "This item should a bread");
            Assert.AreEqual("Apple", apple.Name, "This item should an apple");
            Assert.AreEqual("Banana", banana.Name, "This item should a banana");
        }

        [TestMethod]
        public void ScanItems()
        {
            // Assert invalid Item
            var isValid = _cashRegister.ScanItem(new SaleItem { Code = "xxx"});
            Assert.IsFalse(isValid, "This should be an invalid item");

            AssertAndScanValidItem("BRD", 1, 1);
        }

        [TestMethod]
        public void ScanItemShouldAddToTotal()
        {
            AssertTotal(0);

            AssertAndScanValidItem("BRD", 2, 2);

            AssertTotal(10);

            AssertAndScanValidItem("APL", 2, 2);

            AssertTotal(30);

        }

        private void AssertTotal(decimal expected)
        {
            var total = _cashRegister.GetTotal();
            Assert.AreEqual(expected, total, $"This should add up to {expected}");
        }

        private void AssertAndScanValidItem(string code, int quantity, decimal weight)
        {
            var isValid = _cashRegister.ScanItem(new SaleItem { Code = code, Quantity = quantity, Weight = weight });
            Assert.IsTrue(isValid, "This should be a valid item");
        }

        [TestMethod]
        public void ShouldProvideListOfItems()
        {
            AssertFirstTransaction();            
            AssertSecondTransaction();
        }

        private void AssertFirstTransaction()
        {
            AssertAndScanValidItem("BRD", 1, 1);
            AssertAndScanValidItem("APL", 1, 1);
            var sale = _cashRegister.CloseSale();
            var items = sale.Items;
            Assert.AreEqual(2, items.Count(), "Two Items expected");

            SaleItem bread = items.First(f => f.Code == "BRD");
            SaleItem apple = items.First(f => f.Code == "APL");

            Assert.AreEqual("BRD", bread.Code, "This item should a BRD");
            Assert.AreEqual("APL", apple.Code, "This item should an APL");
        }

        private void AssertSecondTransaction()
        {
            AssertAndScanValidItem("BRD", 1, 2);
            AssertAndScanValidItem("APL", 1, 2);
            AssertAndScanValidItem("BNA", 1, 2);
            var sale = _cashRegister.CloseSale();
            var items = sale.Items;

            Assert.AreEqual(3, items.Count(), "Two Items expected");

            SaleItem bread = items.First(f => f.Code == "BRD");
            SaleItem apple = items.First(f => f.Code == "APL");
            SaleItem banana = items.First(f => f.Code == "BNA");

            Assert.AreEqual("BRD", bread.Code, "This item should a BRD");
            Assert.AreEqual("APL", apple.Code, "This item should an APL");
            Assert.AreEqual("BNA", banana.Code, "This item should an BNA");
        }

        [TestMethod]
        public void Apply10PercentageCoupon() {
            AssertAndScanValidItem("BRD", 1, 1);
            AssertAndScanValidItem("APL", 1, 1);
            AssertAndScanValidItem("BNA", 1, 1);

            AssertTotal(30);

            _cashRegister.ApplyCoupon("10Percent");

            AssertTotal(27);

        }

        [TestMethod]
        public void Apply15PercentageCoupon()
        {
            AssertAndScanValidItem("BRD", 1, 2);
            AssertAndScanValidItem("APL", 1, 2);
            AssertAndScanValidItem("BNA", 1, 2);

            AssertTotal(30);

            _cashRegister.ApplyCoupon("15Percent");

            AssertTotal(25.5m);

        }

        [TestMethod]
        public void ApplyGetOneFreeCoupon()
        {
            AssertAndScanValidItem("BRD", 1, 2);
            AssertAndScanValidItem("APL", 1, 1);
            AssertAndScanValidItem("BNA", 3, 2);

            _cashRegister.ApplyCoupon("GetFreeBanana");

            AssertTotal(50);
        }
    }
}
