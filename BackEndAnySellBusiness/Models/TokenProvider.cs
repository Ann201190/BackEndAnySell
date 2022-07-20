using System.Collections.Generic;

namespace BackEndAnySellBusiness.Models
{
    public class TokenProvider
    {
        public void AddTicketTokens(Dictionary<string, object> tokens, TicketPrintModel ticket)
        {
            tokens.Add("StoreName", ticket.StoreName);
            tokens.Add("StoreAddress", ticket.StoreAddress);
            tokens.Add("CashierName", $"Кассир: {ticket.CashierName}");
            tokens.Add("OrderNumber", $"Чек: {ticket.OrderNumber}");
        }

        public void AddPriceHolderTokens(Dictionary<string, object> tokens, PriceHolderPrintModel priceHolder)
        {
            tokens.Add("StoreName", priceHolder.StoreName);
            tokens.Add("Barcode", priceHolder.Barcode);
            tokens.Add("CurrentDate", priceHolder.CurrentDate);
            tokens.Add("DefaultPrice", priceHolder.DefaultPrice);
            tokens.Add("PriceWithDiscount", priceHolder.PriceWithDiscount);
            tokens.Add("ProductName", priceHolder.ProductName);
        }
    }
}
