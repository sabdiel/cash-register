using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Coupons
{
    public class GetOneFreeCoupon : ICoupon
    {
        public string Code { get; set; }
        public CouponType Type { get; set; }
        public string BuyItemCode { get; set; }
        public int BuyItemQuantity { get; set; }
        public string FreeItemCode { get; set; }
        public IEnumerable<ShoppingItem> ItemsInStore { get; set; }

        public GetOneFreeCoupon(string code, CouponType type, string buyItemCode, int buyItemQuantity, string freeItemCode, IEnumerable<ShoppingItem> itemsInStore)
        {
            Code = code;
            Type = type;
            BuyItemCode = buyItemCode;
            BuyItemQuantity = buyItemQuantity;
            FreeItemCode = freeItemCode;
            ItemsInStore = itemsInStore;
        }

        public decimal ApplyDiscount(IEnumerable<SaleItem> shoppingList, decimal total)
        {
            var quantity = shoppingList.Where(c => c.Code == BuyItemCode)
                            .Sum(s => s.Quantity);

            if(quantity >= BuyItemQuantity)
            {
                var discount = ItemsInStore.FirstOrDefault(f => f.Code == FreeItemCode);
                return total - discount.Price;
            }

            return total;
        }
    }
}
