using Mango.WEB.Entities.Stock;
using Mango.WEB.Models.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mango.WEB.Interfaces.Repositories.Stock
{
    public interface IStockRepository
    {
        Task<StockEntity> CreateAsync(StockEntity stock);
        Task<bool> DeleteAsync(Guid stockUID, Guid loggedInUserUID);
        Task<IList<StockEntity>> GetAsync(StockType filter = StockType.General);
        Task<IList<StockEntity>> GetByUserAsync(Guid userUID, StockType filter = StockType.General);
        Task<StockEntity> GetAsync(Guid stockUID);
        Task<bool> UpdateAsync(Guid stockUID, StockEntity updatedStock, Guid loggedInUserUID);
    }
}
