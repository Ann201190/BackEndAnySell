using System;

namespace BackEndAnySellBusiness.Models
{
    public class PriceHolderPrintModel
    {
        public string ProductName { get; set; }
        public string StoreName { get; set; }
        public string PriceWithDiscount { get; set; }
        public string DefaultPrice { get; set; }
        public string Barcode { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.UtcNow;
    }
}
