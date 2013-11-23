namespace Shopping.Models
{

    public class ReceiptItem
    {
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public string Desc { get; set; }

        public ReceiptItem(string sku, decimal price, string desc = null)
        {
            Sku = sku;
            Price = price;
            Desc = desc ?? sku;
        }

    }
}