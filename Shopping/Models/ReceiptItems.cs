using System.Collections.Generic;
using System.Linq;

namespace Shopping.Models
{
    public class ReceiptItems
    {
        private readonly List<ReceiptItem> _receiptItems = new List<ReceiptItem>();

        public decimal TotalPrice
        {
            get
            { 
                return _receiptItems.Sum(r => r.Price);
            }
        }

        public IEnumerable<ReceiptItem> ItemEnumerator
        {
            get {
                return _receiptItems.Select(receiptItem => new ReceiptItem(receiptItem.Sku, receiptItem.Price, receiptItem.Desc));
            }
        }

        public void AddReceiptitem(string sku, decimal price)
        {
            _receiptItems.Add(new ReceiptItem(sku, price));
        }
    }
}
