using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Shopping
{
    internal class CheckoutTill : ICheckoutTill
    {
        private readonly List<Item> _items = new List<Item>();
        private readonly List<PricingRule> _rules = new List<PricingRule>();

        public decimal CalcTotal()
        {
            var total = 0m;
            foreach (var item in _items)
            {
                var price = _rules.Single(r => r.Sku == item.Sku).Price;
                total += price;
            }
            return total;
        }

        public void AddItem(string sku)
        {
            _items.Add(new Item(sku));
        }


        public void AddPricingRule(string sku, decimal price, string rule)
        {
            _rules.Add(new PricingRule(sku,price,rule));
        }
    }

    internal class PricingRule
    {
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public string Rule { get; set; }

        public PricingRule(string sku, decimal price, string rule)
        {
            Sku = sku;
            Price = price;
            Rule = rule;
        }
    }

    internal class Item
    {
        public string Sku { get; set; }

        public Item(string sku)
        {
            Sku = sku;
        }
    }
}