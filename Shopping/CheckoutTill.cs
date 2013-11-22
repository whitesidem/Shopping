using System.Collections.Generic;
using System.Linq;

namespace Shopping
{
    internal class CheckoutTill : ICheckoutTill
    {
        private readonly List<Item> _items = new List<Item>();

        public decimal CalcTotal()
        {
            return _items.Sum(i => i.Price);
        }

        public void AddItem(string sku, decimal price)
        {
            _items.Add(new Item(sku, price));
        }
    }

    internal class Item
    {
        public string Sku { get; set; }
        public decimal Price { get; set; }

        public Item(string sku, decimal price)
        {
            Sku = sku;
            Price = price;
        }
    }
}