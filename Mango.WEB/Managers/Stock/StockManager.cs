using Mango.WEB.Adapters.Stock;
using Mango.WEB.Entities.Stock;
using Mango.WEB.Helpers;
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
        private const string ENTITY_NAME = "Stock";

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
                ErrorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} create {ENTITY_NAME}."
            };
        }

        public async Task<BaseResponse> DeleteAsync(UserUIDAndUIDRequest request)
        {
            BaseResponse _Response = new BaseResponse();

            if (request.UID == Guid.Empty || !await __StockRepository.DeleteAsync(request.UID, request.UserUID))
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} delete {ENTITY_NAME}.";
            }

            return _Response;
        }

        public async Task<StocksResponse> GetAsync(GetStocksRequest request)
        {
            IList<StockEntity> _Entities = await __StockRepository.GetAsync(request?.StockType ?? StockType.General);

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
                _Response.ErrorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} retrieve {ENTITY_NAME}.";
            }

            StockEntity _StockEntity = await __StockRepository.GetAsync(request.UID);

            if (_StockEntity == null)
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} retrieve {ENTITY_NAME}.";
            }

            return _StockEntity.ToResponse() ?? _Response;
        }
        public async Task<StocksResponse> GetByUserAsync(GetStocksByUserRequest request)
        {
            IList<StockEntity> _Entities = await __StockRepository.GetByUserAsync(request.UserUID, request?.StockType ?? StockType.General);

            return new StocksResponse
            {
                Stocks = _Entities.ToResponse().ToList()
            };
        }

        public async Task<BaseResponse> UpdateAsync(UpdateStockRequest request)
        {
            BaseResponse _Response = new BaseResponse();

            if (request.UID == Guid.Empty || !await __StockRepository.UpdateAsync(request.UID, request.ToEntity(), request.UserUID))
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} retrieve {ENTITY_NAME}.";
            }

            return _Response;
        }
    }
}
