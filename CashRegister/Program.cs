using CashRegister.Coupons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister
{
    class Program
    {
        public static IEnumerable<ShoppingItem> ShoppingItems { get; set; }
        public static IEnumerable<ICoupon> Coupons { get; set; }
        public static List<SaleItem> SaleItems { get; set; }
        private static CashRegister _cashRegister;
        private static SaleItem _newSaleItem;

        const string START_SCANNING = "1";
        const string EXIT = "2";


        static void Main(string[] args)
        {

            _cashRegister = new CashRegister();
            SetPredifinedData();
            DisplayAvailableItemsAndCouponsInStore();

            Console.WriteLine("\n-------------------------------------");
            string option = string.Empty;

            option = GetUserOptionSelection();
            ExitIfNecessary(option);

            if (option == START_SCANNING)
            {
                ManageUserInteraction();
            }

            Console.ReadLine();
        }

        private static void ManageUserInteraction()
        {
            while (true)
            {
                InputItemCode();

                InputItemQuantity();

                InputItemWeight();

                // Validate if the item code is in the predefined item list
                if (!_cashRegister.ScanItem(_newSaleItem))
                {
                    Console.WriteLine("Invalid Code!!");
                    continue;
                }

                bool hasCoupon = HasACoupon();

                if (hasCoupon)
                {
                    Console.WriteLine("Enter Coupon Code: ");
                    var couponCode = Console.ReadLine();
                    _cashRegister.ApplyCoupon(couponCode);
                }

                bool proceed = CanProceed();

                if (!proceed) break;
            }

            var sale = _cashRegister.CloseSale();

            Console.WriteLine("************************* Receipt ****************************");
            foreach (var saleItem in sale.Items.Select((value, index) => new { value, index }))
            {
                var item = saleItem.value;
                Console.WriteLine($"item:  {item.Code} Qty: {item.Quantity} lbs: {item.Weight}");
            }

            Console.WriteLine($"Total: {sale.Total}");
        }

        private static bool CanProceed()
        {
            Console.WriteLine("Continue scanning? (y/n)");
            var proceed = Console.ReadLine().ToLower() == "y";
            return proceed;
        }

        private static bool HasACoupon()
        {
            Console.WriteLine("Any Coupon? (y/n)");
            var hasCoupon = Console.ReadLine().ToLower() == "y";
            return hasCoupon;
        }

        private static void InputItemWeight()
        {
            // Ask for the lbs (pounds)
            Console.WriteLine($"Item #{_newSaleItem.Code} weight (lbs): ");
            int.TryParse(Console.ReadLine(), out int weight);
            _newSaleItem.Weight = weight;
        }

        private static void InputItemQuantity()
        {
            // Ask for the quantity 
            Console.WriteLine($"Item #{_newSaleItem.Code} quantity: ");
            int.TryParse(Console.ReadLine(), out int quantity);
            _newSaleItem.Quantity = quantity;
        }

        private static void InputItemCode()
        {
            // Simulate scanning an item by asking user for the item code.
            _newSaleItem = new SaleItem();
            Console.WriteLine("Enter the Item Code: ");
            _newSaleItem.Code = Console.ReadLine();
        }

        private static void ExitIfNecessary(string option)
        {
            if (option == EXIT)
                Environment.Exit(0);
        }

        private static string GetUserOptionSelection()
        {
            string option;
            while (true)
            {
                DisplaySelectionOptions();
                option = Console.ReadLine();
                if (option == EXIT || option == START_SCANNING)
                    break;
                else
                    Console.WriteLine("Invalid selection.!");
            }

            return option;
        }

        private static void DisplayAvailableItemsAndCouponsInStore()
        {
            DisplayItems();
            Console.WriteLine();
            Console.WriteLine("*************************");
            DisplayCoupons();
        }

        private static void SetPredifinedData()
        {
            Predefined predefined = new Predefined();
            SaleItems = new List<SaleItem>();
            ShoppingItems = predefined.GetAllItems();
            Coupons = predefined.GetAllCoupons();
        }

        private static void DisplaySelectionOptions()
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to Wilson's Store ... \n");
            Console.WriteLine("1) Start Scanning");
            Console.WriteLine("2) Exit");
        }

        private static void DisplayItems()
        {
            Console.WriteLine("*************************");
            Console.WriteLine("Availabe Items in Store");
            Console.WriteLine();

            foreach (var item in ShoppingItems)
                Console.WriteLine($"Item #{item.Code}: {item.Name} - ${item.Price}");
        }

        private static void DisplayCoupons()
        {
            Console.WriteLine("Availabe Coupons in Store");
            Console.WriteLine();

            foreach (var coupon in Coupons)
                Console.WriteLine($"Code: {coupon.Code} - {coupon.Type}");
        }
    }
}
