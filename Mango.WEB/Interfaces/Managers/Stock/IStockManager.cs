using Mango.WEB.Models.Base.Request;
using Mango.WEB.Models.Base.Response;
using Mango.WEB.Models.Stock.Request;
using Mango.WEB.Models.Stock.Response;
using System.Threading.Tasks;

namespace Mango.WEB.Interfaces.Managers.Stock
{
    public interface IStockManager
    {
        Task<StockResponse> CreateAsync(CreateStockRequest request);
        Task<BaseResponse> DeleteAsync(UserUIDAndUIDRequest request);
        Task<StocksResponse> GetAsync(GetStocksRequest request = null);
        Task<StockResponse> GetAsync(UIDRequest request);
        Task<BaseResponse> UpdateAsync(UpdateStockRequest request);
    }
}
