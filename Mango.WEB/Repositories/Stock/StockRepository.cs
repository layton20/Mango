using Mango.WEB.Entities.Stock;
using Mango.WEB.Interfaces.Repositories.Stock;
using Mango.WEB.Models;
using Mango.WEB.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.WEB.Repositories.Stock
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationContext __Context;

        public StockRepository(ApplicationContext context)
        {
            __Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<StockEntity> CreateAsync(StockEntity stock)
        {
            if (stock == null || stock.UID == Guid.Empty)
            {
                return null;
            }

            await __Context.Stocks.AddAsync(stock);
            bool _Added = await __Context.SaveChangesAsync() > 0;

            return _Added ? stock : null;
        }

        public async Task<bool> DeleteAsync(Guid stockUID, Guid loggedInUserUID)
        {
            if (stockUID == Guid.Empty)
            {
                return false;
            }

            StockEntity _Stock = await __Context.Stocks.FirstOrDefaultAsync(x => x.UID == stockUID);

            if (_Stock == null || (_Stock.UserUID != loggedInUserUID && loggedInUserUID != Guid.Empty))
            {
                return false;
            }

            __Context.Stocks.Remove(_Stock);

            return await __Context.SaveChangesAsync() > 0;
        }

        public async Task<IList<StockEntity>> GetAsync(StockType filter = StockType.General)
        {
            if (filter == StockType.General)
            {
                return await __Context.Stocks?.ToListAsync() ?? Enumerable.Empty<StockEntity>().ToList();
            }

            return await __Context.Stocks.Where(x => x.StockType == filter)?.ToListAsync() ?? Enumerable.Empty<StockEntity>().ToList();
        }

        public async Task<StockEntity> GetAsync(Guid stockUID)
        {
            return await __Context.Stocks.FirstOrDefaultAsync(x => x.UID == stockUID);
        }

        public async Task<IList<StockEntity>> GetByUserAsync(Guid userUID, StockType filter = StockType.General)
        {
            if (filter == StockType.General)
            {
                return await __Context.Stocks?.Where(x => x.UserUID == userUID).ToListAsync() ?? Enumerable.Empty<StockEntity>().ToList();
            }

            return await __Context.Stocks.Where(x => x.StockType == filter && x.UserUID == userUID)?.ToListAsync() ?? Enumerable.Empty<StockEntity>().ToList();
        }

        public async Task<bool> UpdateAsync(Guid stockUID, StockEntity updatedStock, Guid loggedInUserUID)
        {
            if (stockUID == Guid.Empty || updatedStock.UID != stockUID)
            {
                return false;
            }

            StockEntity _StockEntity = await __Context.Stocks.FirstOrDefaultAsync(x => x.UID == stockUID);

            if (_StockEntity == null || (_StockEntity.UserUID != loggedInUserUID && loggedInUserUID != Guid.Empty))
            {
                return false;
            }

            _StockEntity.Name = updatedStock.Name;
            _StockEntity.Description = updatedStock.Description;
            _StockEntity.Quantity = updatedStock.Quantity;
            _StockEntity.StockType = updatedStock.StockType;
            _StockEntity.AmendedTimestamp = DateTime.Now;

            return await __Context.SaveChangesAsync() > 0;
        }
    }
}
