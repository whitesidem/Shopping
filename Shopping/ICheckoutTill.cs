using System;

namespace Shopping
{
    internal interface ICheckoutTill
    {
        Decimal CalcTotal();
        void AddItem(string sku);
        void AddPricingRule(string sku, decimal price, string rule);
    }
}