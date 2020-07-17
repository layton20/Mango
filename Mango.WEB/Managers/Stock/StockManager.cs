using Mango.WEB.Adapters.Stock;
using Mango.WEB.Entities.Stock;
using Mango.WEB.Interfaces.Managers.Stock;
using Mango.WEB.Interfaces.Repositories.Stock;
using Mango.WEB.Models.Base.Request;
using Mango.WEB.Models.Base.Response;
using Mango.WEB.Models.Enums;
using Mango.WEB.Models.Stock.Request;
using Mango.WEB.Models.Stock.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.WEB.Managers.Stock
{
    public class StockManager : IStockManager
    {
        private readonly IStockRepository __StockRepository;
        private const string ERROR_PREFIX = "Unable to";

        public StockManager(IStockRepository stockRepository)
        {
            __StockRepository = stockRepository ?? throw new ArgumentNullException(nameof(stockRepository));
        }

        public async Task<StockResponse> CreateAsync(CreateStockRequest request)
        {
            StockEntity _CreatedEntity = await __StockRepository.CreateAsync(request.ToEntity());

            return _CreatedEntity.ToResponse() ?? new StockResponse
            {
                Success = false,
                ErrorMessage = $"{ERROR_PREFIX} create Stock."
            };
        }

        public async Task<BaseResponse> DeleteAsync(UIDRequest request)
        {
            BaseResponse _Response = new BaseResponse();

            if (request.UID == Guid.Empty || !await __StockRepository.DeleteAsync(request.UID))
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"{ERROR_PREFIX} delete Stock.";
            }

            return _Response;
        }

        public async Task<StocksResponse> GetAsync(GetStocksRequest request)
        {
            IList<StockEntity> _Entities = await __StockRepository.GetAsync(request?.StockType ?? StockType.Unknown);

            return new StocksResponse
            {
                Stocks = _Entities.ToResponse().ToList()
            };
        }

        public async Task<StockResponse> GetAsync(UIDRequest request)
        {
            StockResponse _Response = new StockResponse();

            if (request.UID == Guid.Empty)
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"{ERROR_PREFIX} retrieve Stock.";
            }

            StockEntity _StockEntity = await __StockRepository.GetAsync(request.UID);

            if (_StockEntity == null)
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"{ERROR_PREFIX} retrieve Stock.";
            }

            return _StockEntity.ToResponse() ?? _Response;
        }

        public async Task<BaseResponse> UpdateAsync(UpdateStockRequest request)
        {
            BaseResponse _Response = new BaseResponse();

            if (request.UID == Guid.Empty || !await __StockRepository.UpdateAsync(request.UID, request.ToEntity()))
            {
                _Response.Success = false; 
                _Response.ErrorMessage = $"{ERROR_PREFIX} retrieve Stock.";
            }

            return _Response;
        }
    }
}
