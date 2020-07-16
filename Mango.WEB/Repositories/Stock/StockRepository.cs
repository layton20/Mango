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

        public async Task<bool> DeleteAsync(Guid stockUID)
        {
            if (stockUID == Guid.Empty)
            {
                return false;
            }

            StockEntity _Stock = await __Context.Stocks.FirstOrDefaultAsync(x => x.UID == stockUID);

            if (_Stock == null)
            {
                return false;
            }

            __Context.Stocks.Remove(_Stock);

            return await __Context.SaveChangesAsync() > 0;
        }

        public async Task<IList<StockEntity>> GetAsync(StockType filter = StockType.Unknown)
        {
            if (filter == StockType.Unknown)
            {
                return await __Context.Stocks?.ToListAsync() ?? Enumerable.Empty<StockEntity>().ToList();
            }

            return await __Context.Stocks.Where(x => x.StockType == filter)?.ToListAsync() ?? Enumerable.Empty<StockEntity>().ToList();
        }

        public async Task<StockEntity> GetAsync(Guid stockUID)
        {
            return await __Context.Stocks.FirstOrDefaultAsync(x => x.UID == stockUID);
        }

        public async Task<bool> UpdateAsync(Guid stockUID, StockEntity updatedStock)
        {
            if (stockUID == Guid.Empty || updatedStock.UID != stockUID)
            {
                return false;
            }

            StockEntity _StockEntity = await __Context.Stocks.FirstOrDefaultAsync(x => x.UID == stockUID);

            if (_StockEntity == null)
            {
                return false;
            }

            _StockEntity = updatedStock;
            return await __Context.SaveChangesAsync() > 0;
        }
    }
}
