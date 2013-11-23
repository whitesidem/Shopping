using System;
using System.Collections.Generic;
using Shopping.Models;

namespace Shopping.Interfaces
{
    internal interface ICheckoutTill
    {
        void OutputReceipt();
        void ScanItem(string sku);
    }
}