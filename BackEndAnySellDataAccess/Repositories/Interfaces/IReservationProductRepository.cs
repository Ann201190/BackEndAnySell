using BackEndAnySellDataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellDataAccess.Repositories.Interfaces
{
    public interface IReservationProductRepository
    {
        Task<bool> AddAsync(List<ReservationProduct> reservationProducts);
    }
}
