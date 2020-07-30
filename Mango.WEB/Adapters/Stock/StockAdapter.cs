using Mango.WEB.Areas.Stock.Models.Stock;
using Mango.WEB.Entities.Stock;
using Mango.WEB.Models.Stock.Request;
using Mango.WEB.Models.Stock.Response;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
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
                StockType = request.StockType,
                UserUID = request.UserUID
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
                StockType = entity.StockType,
                CreatedTimestamp = entity.CreatedTimestamp,
                AmendedTimestamp = entity.AmendedTimestamp
            };
        }

        internal static IEnumerable<StockResponse> ToResponse(this IEnumerable<StockEntity> entities)
        {
            return entities != null || entities.Count() > 0 ? entities.Select(ToResponse) : Enumerable.Empty<StockResponse>();
        }

        internal static StockEntity ToEntity(this StockResponse response)
        {
            return response == null ? null : new StockEntity
            {
                UID = response.UID,
                Name = response.Name,
                Description = response.Description,
                Quantity = response.Quantity,
                StockType = response.StockType,
                CreatedTimestamp = response.CreatedTimestamp,
                AmendedTimestamp = response.AmendedTimestamp
            };
        }

        internal static IEnumerable<StockEntity> ToEntity(this IEnumerable<StockResponse> responses)
        {
            return responses != null && responses.Count() > 0 ? responses.Select(ToEntity) : Enumerable.Empty<StockEntity>();
        }

        internal static CreateStockRequest ToRequest(this CreateViewModel viewModel, Guid userUID)
        {
            return viewModel == null ? null : new CreateStockRequest
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                StockType = viewModel.StockType,
                Quantity = viewModel.Quantity,
                UserUID = userUID
            };
        }

        internal static StockViewModel ToViewModel(this StockResponse response)
        {
            return response == null ? null : new StockViewModel
            {
                UID = response.UID,
                Name = response.Name,
                Description = response.Description,
                Quantity = response.Quantity,
                StockType = response.StockType,
                AmendedTimestamp = response.AmendedTimestamp
            };
        }

        internal static IEnumerable<StockViewModel> ToViewModel(this IList<StockResponse> responses)
        {
            return responses != null && responses.Count > 0 ? responses.Select(ToViewModel) : Enumerable.Empty<StockViewModel>();
        }

        internal static EditViewModel ToEditViewModel(this StockResponse response)
        {
            return response == null ? null : new EditViewModel
            {
                Description = response.Description,
                Name = response.Name,
                Quantity = response.Quantity,
                StockType = response.StockType,
                StockUID = response.UID
            };
        }

        internal static UpdateStockRequest ToUpdateRequest(this EditViewModel viewModel, Guid userUID)
        {
            return viewModel == null ? null : new UpdateStockRequest
            {
                Description = viewModel.Description,
                Name = viewModel.Name,
                Quantity = viewModel.Quantity,
                StockType = viewModel.StockType,
                UID = viewModel.StockUID,
                UserUID = userUID
            };
        }

        internal static DeleteViewModel ToDeleteViewModel(this StockResponse response)
        {
            return response == null ? null : new DeleteViewModel
            {
                AmendedTimestamp = response.AmendedTimestamp,
                Description = response.Description,
                Name = response.Name,
                Quantity = response.Quantity,
                StockType = response.StockType,
                UID = response.UID
            };
        }
    }
}
