using System;

namespace Shopping
{
    internal interface ICheckoutTill
    {
        Decimal CalcTotal();
        void AddItem(string sku, decimal price);
    }
}