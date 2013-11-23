using System.Collections.Generic;
using Shopping.Models;

namespace Shopping
{
    internal class SpecialRuleEngine
    {
        private readonly PricingRules _rules;
        private readonly Dictionary<string, int> _ruleAccumulator = new Dictionary<string, int>();


        public SpecialRuleEngine(PricingRules rules)
        {
            _rules = rules;
        }

        public void MonitorandApplyDiscountForSku(string sku, ReceiptItems receiptItems)
        {
            var rule = _rules.GetRuleBySku(sku);
            if (!rule.HasSpecialRuleForSku()) return;
            MonitorAndApplyRule(rule, receiptItems);
        }

        private void MonitorAndApplyRule(PricingRule rule, ReceiptItems receiptItems)
        {
            IncrementRuleAccumulatorForRule(rule);
            if (_ruleAccumulator[rule.Sku] < rule.QuailfyingQty)
            {
                return;
            }
            ApplyRule(rule, receiptItems);
        }

        private void ApplyRule(PricingRule rule, ReceiptItems receiptItems)
        {
            receiptItems.AddReceiptDiscount(rule.Sku, rule.DiscountAmount, rule.Rule);
            ResetRuleAccumulatorForRule(rule);
        }

        private void IncrementRuleAccumulatorForRule(PricingRule rule)
        {
            if (!_ruleAccumulator.ContainsKey(rule.Sku))
            {
                _ruleAccumulator.Add(rule.Sku, 0);
            }
            _ruleAccumulator[rule.Sku]++;
        }

        private void ResetRuleAccumulatorForRule(PricingRule rule)
        {
            _ruleAccumulator.Remove(rule.Sku);
        }

    }
}