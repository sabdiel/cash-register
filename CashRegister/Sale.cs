using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister
{
    public class Sale
    {
        public decimal Total { get; set; }
        public List<SaleItem> Items { get; set; }

        public Sale(List<SaleItem> items, decimal total)
        {
            Items = items;
            Total = total;
        }
    }
}
