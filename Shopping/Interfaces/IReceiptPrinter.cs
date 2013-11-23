using System.Collections.Generic;
using Shopping.Models;

namespace Shopping.Interfaces
{
    internal interface IReceiptPrinter
    {
        void ListReciptitems(List<ReceiptItem> toList);
        void TotalPrice(decimal totalPrice);
    }
}