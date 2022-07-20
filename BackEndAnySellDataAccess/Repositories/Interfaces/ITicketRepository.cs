using BackEndAnySellDataAccess.Entities;
using System.Threading.Tasks;

namespace BackEndAnySellDataAccess.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task<Ticket> GetTicketAsync();
        Task<Ticket> GetPriceHolderAsync();
    }
}
