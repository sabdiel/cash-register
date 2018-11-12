using CashRegister.Coupons;
using System.Collections.Generic;
using System.Linq;

namespace CashRegister
{
    public class CashRegister
    {
        private IEnumerable<ShoppingItem> _itemsInStore;
        private IEnumerable<ICoupon> _couponsInStore;
        private List<SaleItem> _saleItems;

        private decimal _total;

        public CashRegister()
        {
            Predefined predefined = new Predefined();
            _saleItems = new List<SaleItem>();
            _itemsInStore = predefined.GetAllItems();
            _couponsInStore = predefined.GetAllCoupons();
            _total = 0;
        }

        public IEnumerable<ShoppingItem> GetAvailabeItemsInStore()
        {
            return _itemsInStore;
        }

        public IEnumerable<ICoupon> GetAvailabeCouponsInStore()
        {
            return _couponsInStore;
        }

        public bool ScanItem(SaleItem saleItem)
        {
            var scannedCode = saleItem.Code.ToLower().Trim();
            var item = _itemsInStore.FirstOrDefault(f => f.Code.ToLower().Trim() == scannedCode);

            if (item != null)
            {
                _saleItems.Add(saleItem);
                _total += item.Price * saleItem.Quantity;
                return true;
            }

            return false;
        }

        public decimal GetTotal()
        {
            return _total;
        }        

        public Sale CloseSale()
        {
            Sale sale = new Sale(_saleItems, _total);
            _saleItems = new List<SaleItem>();
            _total = 0;
            return sale;
        }

        public void ApplyCoupon(string couponCode)
        {
            var coupon = _couponsInStore.FirstOrDefault(f => f.Code.ToLower() == couponCode.ToLower());
            _total = coupon.ApplyDiscount(_saleItems, _total);
        }
    }
}
