using Mango.WEB.Entities.Stock;
using Mango.WEB.Models.Stock.Request;
using Mango.WEB.Models.Stock.Response;
using System.Collections.Generic;
using System.Linq;

namespace Mango.WEB.Adapters.Stock
{
    internal static class StockAdapter
    {
        internal static StockEntity ToEntity(this CreateStockRequest request)
        {
            return request == null ? null : new StockEntity
            {
                Name = request.Name,
                Description = request.Description,
                Quantity = request.Quantity,
                StockType = request.StockType
            };
        }

        internal static StockEntity ToEntity(this UpdateStockRequest request)
        {
            return request == null ? null : new StockEntity
            {
                UID = request.UID,
                Name = request.Name,
                Description = request.Description,
                Quantity = request.Quantity,
                StockType = request.StockType
            };
        }

        internal static StockResponse ToResponse(this StockEntity entity)
        {
            return entity == null ? null : new StockResponse
            {
                UID = entity.UID,
                Name = entity.Name,
                Description = entity.Description,
                Quantity = entity.Quantity,
                CreatedTimestamp = entity.CreatedTimestamp,
                AmendedTimestamp = entity.AmendedTimestamp
            };
        }

        internal static IEnumerable<StockResponse> ToResponse(this IEnumerable<StockEntity> entities)
        {
            return entities != null || entities.Count() > 0 ? entities.Select(ToResponse) : Enumerable.Empty<StockResponse>();
        }
    }
}
