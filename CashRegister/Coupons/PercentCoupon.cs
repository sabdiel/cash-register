using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Coupons
{
    public class PercentCoupon : ICoupon
    {
        public string Code { get; set; }
        public CouponType Type { get; set; }
        public decimal Percentage { get; set; }

        public PercentCoupon(string code, CouponType type, decimal percentage)
        {
            Code = code;
            Type = type;
            Percentage = percentage;
        }

        public decimal ApplyDiscount(IEnumerable<SaleItem> shoppingList, decimal total)
        {
            return total - (Percentage * total);            
        }
    }
}
