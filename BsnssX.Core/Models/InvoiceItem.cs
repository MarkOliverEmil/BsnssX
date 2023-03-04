namespace BsnssX.Core.Models
{
    public class InvoiceItem 
    {
        public string Id { get; set; }
        
        public BookableItem BookableItem { get; set; }
        
        public decimal Price { get => BookableItem.Price; }

        public decimal Quantity { get; set; }

        public decimal SumPrice { get => BookableItem.Price * Quantity; }
        public string SumPriceString { get => $"{SumPrice} {BookableItem.Currency}"; }

        public static InvoiceItem CreateFrom(BookableItem b, decimal quant, string description2 = null)
        {
            InvoiceItem item = new InvoiceItem()
            {
                BookableItem = b,
                Quantity = quant,               
            };
            item.BookableItem.Description2 = description2;
            return item;
        }
    }
}
