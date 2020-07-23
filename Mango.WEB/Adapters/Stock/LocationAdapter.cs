using Mango.WEB.Areas.Stock.Models.Location;
using Mango.WEB.Areas.Stock.Models.Stock;
using Mango.WEB.Entities.Stock;
using Mango.WEB.Models.Base.Request;
using Mango.WEB.Models.Stock.Request;
using Mango.WEB.Models.Stock.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mango.WEB.Adapters.Stock
{
    internal static class LocationAdapter
    {
        internal static LocationViewModel ToViewModel(this LocationResponse response)
        {
            return response == null ? null : new LocationViewModel
            {
                Name = response.Name,
                Description = response.Description,
                Address = response.Address,
                Floor = response.Floor,
                UID = response.UID,
                UserUID = response.UserUID
            };
        }

        internal static IEnumerable<LocationViewModel> ToViewModel(this IList<LocationResponse> responses)
        {
            return responses != null && responses.Count > 0 ? responses.Select(ToViewModel) : Enumerable.Empty<LocationViewModel>();
        }

        internal static LocationEntity ToEntity(this CreateLocationRequest request)
        {
            return request == null ? null : new LocationEntity
            {
                Name = request.Name,
                Description = request.Description,
                Floor = request.Floor,
                Address = request.Address,
                UserUID = request.UserUID
            };
        }

        internal static LocationResponse ToResponse(this LocationEntity entity)
        {
            return entity == null ? null : new LocationResponse
            {
                UID = entity.UID,
                Name = entity.Name,
                Description = entity.Description,
                Floor = entity.Floor,
                Address = entity.Address,
                UserUID = entity.UserUID
            };
        }

        internal static IEnumerable<LocationResponse> ToResponse(this IList<LocationEntity> entities)
        {
            return entities != null && entities.Count > 0 ? entities.Select(ToResponse) : Enumerable.Empty<LocationResponse>();
        }

        internal static CreateLocationRequest ToRequest(this CreateViewModel model)
        {
            return model == null ? null : new CreateLocationRequest
            {
                Name = model.Name,
                Description = model.Description,
                Floor = model.Floor,
                Address = model.Address,
                UserUID = model.UserUID
            };
        }

        internal static LocationEntity ToEntity(this UpdateLocationRequest request)
        {
            return request == null ? null : new LocationEntity
            {
                UID = request.UID,
                Name = request.Name,
                Description = request.Description,
                Floor = request.Floor,
                Address = request.Address,
                UserUID = request.UserUID
            };
        }

        internal static EditViewModel ToEditViewModel(this LocationResponse response)
        {
            return response == null ? null : new EditViewModel
            {
                LocationUID = response.UID,
                Name = response.Name,
                Description = response.Description,
                Floor = response.Floor,
                Address = response.Address
            };
        }

        internal static UpdateLocationRequest ToUpdateRequest(this EditViewModel model, Guid userUID)
        {
            return model == null ? null : new UpdateLocationRequest
            {
                UserUID = userUID,
                UID = model.LocationUID,
                Name = model.Name,
                Description = model.Description,
                Floor = model.Floor,
                Address = model.Address,
            };
        }
    }
}
