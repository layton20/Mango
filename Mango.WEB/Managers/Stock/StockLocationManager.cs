using Mango.WEB.Helpers;
using Mango.WEB.Interfaces.Managers.Stock;
using Mango.WEB.Interfaces.Repositories.Stock;
using Mango.WEB.Models.Base.Response;
using Mango.WEB.Models.Stock.Request;
using System;
using System.Threading.Tasks;

namespace Mango.WEB.Managers.Stock
{
    public class StockLocationManager : IStockLocationManager
    {
        private readonly IStockLocationRepository __StockLocationRepository;

        public StockLocationManager(IStockLocationRepository stockLocationRepository)
        {
            __StockLocationRepository = stockLocationRepository ?? throw new ArgumentNullException(nameof(stockLocationRepository));
        }

        public async Task<BaseResponse> AssignToLocationAsync(AssignToLocationRequest request)
        {
            BaseResponse _Response = new BaseResponse();

            if (!await __StockLocationRepository.AssignToLocationAsync(request.StockUID, request.LocationUID, request.Quantity))
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} assign the stock to the location.";
            }

            return _Response;
        }

        public async Task<BaseResponse> TransferToLocationAsync(TransferToLocationRequest request)
        {
            BaseResponse _Response = new BaseResponse();

            if (!await __StockLocationRepository.TransferToLocationAsync(request.StockLocationUID, request.LocationUID, request.Quantity))
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} tranfer stocks another location.";
            }

            return _Response;
        }

        public async Task<BaseResponse> UnassignedFromLocationAsync(UnassignFromLocationRequest request)
        {
            BaseResponse _Response = new BaseResponse();

            if (!await __StockLocationRepository.UnassignedFromLocationAsync(request.StockLocationUID, request.Quantity))
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} unassign stocks from the location.";
            }

            return _Response;
        }
    }
}
