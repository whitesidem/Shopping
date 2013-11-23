using System.Linq;
using Shopping.Interfaces;
using Shopping.Models;

namespace Shopping
{
    internal class CheckoutTill : ICheckoutTill
    {
        private readonly PricingRules _rules;
        private readonly ReceiptItems _receiptItems;
        private readonly IReceiptPrinter _receiptPrinter;
        private readonly SpecialRuleEngine _specialRuleEngine;

        public CheckoutTill(PricingRules rules, IReceiptPrinter receiptPrinter)
        {
            _rules = rules;
            _specialRuleEngine = new SpecialRuleEngine(_rules);
            _receiptItems = new ReceiptItems();
            _receiptPrinter = receiptPrinter;
        }

        public void ScanItem(string sku)
        {
            var price = _rules.GetPriceForSku(sku);
            _receiptItems.AddReceiptItem(sku, price);
            _specialRuleEngine.MonitorandApplyDiscountForSku(sku, _receiptItems);
        }

        public void OutputReceipt()
        {
            OutputRecieptItems();
            OutputReceiptTotal();
        }


        private void OutputReceiptTotal()
        {
            _receiptPrinter.TotalPrice(_receiptItems.TotalPrice);
        }

        private void OutputRecieptItems()
        {
            _receiptPrinter.ListReciptitems(_receiptItems.ItemEnumerator.ToList());
        }
    }
}