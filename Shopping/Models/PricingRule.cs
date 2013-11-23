using System;

namespace Shopping.Models
{
    public class PricingRule
    {
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public string Rule { get; set; }
        public int QuailfyingQty { get; private set; }
        public decimal DiscountAmount { get; private set; }

        public PricingRule(string sku, decimal price, string rule)
        {
            Sku = sku;
            Price = price;
            Rule = rule;
            ParseRule();
        }

        public bool HasSpecialRuleForSku()
        {
            return string.IsNullOrEmpty(Rule) == false;
        }

        private void ParseRule()
        {
            if (!HasSpecialRuleForSku())
            {
                return;
            }
            var tokens = Rule.Split('>');
            SetQualifyingQty(tokens);
            SetDiscountAmount(tokens);
        }

        private void SetDiscountAmount(string[] tokens)
        {
            var discountedPrice = Decimal.Parse(tokens[1]);
            DiscountAmount = ((QuailfyingQty*Price) - discountedPrice)*-1;
        }

        private void SetQualifyingQty(string[] tokens)
        {
            QuailfyingQty = Int32.Parse(tokens[0]);
        }

    }
}