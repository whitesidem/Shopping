namespace Shopping.Models
{
    public class PricingRule
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
}