using System;
using System.Collections.Generic;
using Shopping.Models;

namespace Shopping.Interfaces
{
    internal interface ICheckoutTill
    {
        void OutputReceiptTotal();
        void AddItem(string sku);
        void OutputRecieptItems();
    }
}