using System;
using System.Threading.Tasks;

namespace Mango.WEB.Interfaces.Repositories.Stock
{
    public interface IStockLocationRepository
    {
        Task<bool> AssignToLocationAsync(Guid stockUID, Guid locationUID, int quantity);
        Task<bool> TransferToLocationAsync(Guid stockLocationUID, Guid locationUID, int quantity);
        Task<bool> UnassignedFromLocationAsync(Guid stockLocationUID, int quantity);
    }
}
