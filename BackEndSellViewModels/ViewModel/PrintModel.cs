using System.Collections.Generic;

namespace BackEndSellViewModels.ViewModel
{
    public class PrintModel
    {
        public string PrinterName { get; set; }
        public IEnumerable<TicketModel> Tickets { get; set; }

        public void Multiplicate(double multiplicator)
        {
            foreach (var ticket in Tickets)
            {
                foreach (var ticketItem in ticket.ItemsInfo)
                {
                    ticketItem.PositionInfo.X = GetValueAfterMultiplicate(ticketItem.PositionInfo.X, multiplicator);
                    ticketItem.PositionInfo.Y = GetValueAfterMultiplicate(ticketItem.PositionInfo.Y, multiplicator);
                    ticketItem.PositionInfo.FontSize = GetValueAfterMultiplicate(ticketItem.PositionInfo.FontSize, multiplicator);
                }
            }
        }

        private int GetValueAfterMultiplicate(int defaultValue, double multiplicator)
        {
            return (int)(defaultValue * multiplicator);
        }
    }
}
