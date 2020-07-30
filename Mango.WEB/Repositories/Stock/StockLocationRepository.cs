using Mango.WEB.Entities.Stock;
using Mango.WEB.Interfaces.Repositories.Stock;
using Mango.WEB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.WEB.Repositories.Stock
{
    public class StockLocationRepository : IStockLocationRepository
    {
        private readonly ApplicationContext __Context;

        public StockLocationRepository(ApplicationContext context)
        {
            __Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> AssignToLocationAsync(Guid stockUID, Guid locationUID, int quantity)
        {
            StockEntity _Stock = await __Context.Stocks.Where(x => x.UID == stockUID).FirstOrDefaultAsync();
            LocationEntity _Location = await __Context.Locations.Where(x => x.UID == locationUID).FirstOrDefaultAsync();

            if (quantity <= 0 || _Stock?.Quantity == null)
            {
                return false;
            }

            IList<StockLocationEntity> _StockLocations = await __Context.StockLocations.Where(x => x.StockUID == stockUID).ToListAsync();
            int _QuantityAssigned = _StockLocations.Sum(x => x.Quantity);

            if (quantity > _Stock.Quantity - _QuantityAssigned)
            {
                return false;
            }

            StockLocationEntity _Entity = new StockLocationEntity
            {
                LocationUID = locationUID,
                StockUID = stockUID,
                Quantity = quantity
            };

            _Stock.Quantity -= quantity;
            __Context.StockLocations.Add(_Entity);
            return await __Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> TransferToLocationAsync(Guid stockLocationUID, Guid locationUID, int quantity)
        {
            StockLocationEntity _StockLocation = await __Context.StockLocations.Where(x => x.UID == stockLocationUID).FirstOrDefaultAsync();
            LocationEntity _NewLocation = await __Context.Locations.Where(x => x.UID == locationUID).FirstOrDefaultAsync();

            if (_StockLocation?.StockUID == null || _StockLocation?.Quantity == null || _NewLocation == null)
            {
                return false;
            } 
            else if (quantity > _StockLocation.Quantity)
            {
                return false;
            }

            _StockLocation.Quantity -= quantity;

            StockLocationEntity _NewStockLocation = new StockLocationEntity
            {
                StockUID = _StockLocation.StockUID,
                LocationUID = locationUID,
                Quantity = quantity
            };

            __Context.StockLocations.Add(_NewStockLocation);
            return await __Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UnassignedFromLocationAsync(Guid stockLocationUID, int quantity)
        {
            StockLocationEntity _StockLocation = await __Context.StockLocations.Where(x => x.UID == stockLocationUID).FirstOrDefaultAsync();

            if (_StockLocation?.Quantity == null)
            {
                return false;
            }
            else if (_StockLocation.Quantity == quantity)
            {
                __Context.StockLocations.Remove(_StockLocation);    
            }
            else
            {
                _StockLocation.Quantity -= quantity;
            }

            return await __Context.SaveChangesAsync() > 0;
        }
    }
}
