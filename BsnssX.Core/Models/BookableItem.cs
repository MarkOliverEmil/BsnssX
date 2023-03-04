namespace BsnssX.Core.Models
{
    public class BookableItem
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string Description2 { get; set; }

        public string Unit { get; set; } // hour, fix, item

        public decimal Price { get; set; } = 0;

        public string Currency { get; set; } = Config.DefaultCurrency;

        public string PriceString { get => $"{Price} {Currency}"; }
    }
}
