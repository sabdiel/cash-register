using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Coupons
{
    public enum CouponType
    {
        Percentage,
        OneFree
    }


    public interface ICoupon
    {
        string Code { get; set; }
        CouponType Type { get; set; }
        decimal ApplyDiscount(IEnumerable<SaleItem> shoppingList, decimal total);
    }
}

