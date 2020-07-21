using Mango.WEB.Adapters.Stock;
using Mango.WEB.Entities.Stock;
using Mango.WEB.Helpers;
using Mango.WEB.Interfaces.Managers.Stock;
using Mango.WEB.Interfaces.Repositories.Stock;
using Mango.WEB.Models.Base.Request;
using Mango.WEB.Models.Base.Response;
using Mango.WEB.Models.Stock.Request;
using Mango.WEB.Models.Stock.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.WEB.Managers.Stock
{
    public class LocationManager : ILocationManager
    {
        private readonly ILocationRepository __LocationRepository;
        private const string ENTITY_NAME = "Kitchen";

        public LocationManager(ILocationRepository locationRepository)
        {
            __LocationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
        }

        public async Task<LocationResponse> CreateAsync(CreateLocationRequest request)
        {
            LocationEntity _CreatedEntity = await __LocationRepository.CreateAsync(request.ToEntity());

            return _CreatedEntity.ToResponse() ?? new LocationResponse
            {
                Success = false,
                ErrorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} create {ENTITY_NAME}."
            };
        }

        public async Task<BaseResponse> DeleteAsync(UIDRequest request)
        {
            BaseResponse _Response = new BaseResponse();

            if (request.UID == Guid.Empty || !await __LocationRepository.DeleteAsync(request.UID))
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} delete {ENTITY_NAME}.";
            }

            return _Response;
        }

        public async Task<LocationResponse> GetAsync(UIDRequest request)
        {
            LocationResponse _Response = new LocationResponse();

            if (request.UID == Guid.Empty)
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} retrieve {ENTITY_NAME}.";
            }

            LocationEntity _LocationEntity = await __LocationRepository.GetAsync(request.UID);

            if (_LocationEntity == null)
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} retrieve {ENTITY_NAME}.";
            }

            return _LocationEntity.ToResponse() ?? _Response;
        }

        public async Task<LocationsResponse> GetByUserAsync(GetLocationsByUserRequest request)
        {
            IList<LocationEntity> _Entities = await __LocationRepository.GetByUserAsync(request.UID);

            return new LocationsResponse
            {
                Locations = _Entities.ToResponse().ToList()
            };
        }

        public async Task<BaseResponse> UpdateAsync(UpdateLocationRequest request)
        {
            BaseResponse _Response = new BaseResponse();

            if (request.UID == Guid.Empty || !await __LocationRepository.UpdateAsync(request.ToEntity()))
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} retrieve {ENTITY_NAME}.";
            }

            return _Response;
        }
    }
}
