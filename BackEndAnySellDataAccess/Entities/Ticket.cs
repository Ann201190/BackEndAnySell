using BackEndAnySellDataAccess.Enums;

namespace BackEndAnySellDataAccess.Entities
{
    public class Ticket : BaseEntity
    {
        public TicketType TicketType { get; set; }
        public string BodyJson { get; set; }
    }
}
