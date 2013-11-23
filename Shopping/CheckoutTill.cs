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

        public CheckoutTill(PricingRules rules, IReceiptPrinter receiptPrinter)
        {
            _rules = rules;
            _receiptItems = new ReceiptItems();
            _receiptPrinter = receiptPrinter;
        }

        public void ScanItem(string sku)
        {
            var price = _rules.GetPriceForSku(sku);
            _receiptItems.AddReceiptitem(sku, price);
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