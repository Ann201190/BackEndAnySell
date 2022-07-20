using BackEndAnySellAccessDataAccess.Context;
using BackEndAnySellDataAccess.Entities;
using BackEndAnySellDataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BackEndAnySellDataAccess.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly CustomDbContext _dbContext;

        public TicketRepository(CustomDbContext dbContext)
        {
            _dbContext = dbContext;
            ModelBuilderExtensions.Initialize(dbContext);
            var builder = new DbContextOptionsBuilder<CustomDbContext>();
        }

        public async Task<Ticket> GetPriceHolderAsync()
        {
            var ticket = await _dbContext.Tickets.FirstOrDefaultAsync(t => t.TicketType == Enums.TicketType.PriceHolder);

            return ticket;
        }

        public async Task<Ticket> GetTicketAsync()
        {
            var ticket = await _dbContext.Tickets.FirstOrDefaultAsync(t => t.TicketType == Enums.TicketType.Ticket);
   
            return ticket;
        }
    }
}
