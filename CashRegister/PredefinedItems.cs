using CashRegister.Coupons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister
{
    public class Predefined
    {
        private IEnumerable<ShoppingItem> _items { get; set; }
        private IEnumerable<ICoupon> _coupons { get; set; }
        public Predefined()
        {
            _items = new List<ShoppingItem>
            {
                new ShoppingItem("BRD", "Bread", 5),
                new ShoppingItem("APL", "Apple", 10),
                new ShoppingItem("BNA", "Banana", 15)
            };

            _coupons = new List<ICoupon>
            {
                new PercentCoupon("10Percent", CouponType.Percentage, 0.10m),
                new PercentCoupon("15Percent", CouponType.Percentage, 0.15m),
                new GetOneFreeCoupon("GetFreeBanana", CouponType.OneFree, "BNA", 3, "APL", _items)
            };
        }

        public IEnumerable<ShoppingItem> GetAllItems()
        {
            return _items;
        }

        public IEnumerable<ICoupon> GetAllCoupons()
        {
            return _coupons;
        }
    }
}
