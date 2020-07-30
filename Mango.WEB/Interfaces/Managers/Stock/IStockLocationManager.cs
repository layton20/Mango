using Mango.WEB.Models.Base.Response;
using Mango.WEB.Models.Stock.Request;
using System.Threading.Tasks;

namespace Mango.WEB.Interfaces.Managers.Stock
{
    public interface IStockLocationManager
    {
        Task<BaseResponse> AssignToLocationAsync(AssignToLocationRequest request);
        Task<BaseResponse> TransferToLocationAsync(TransferToLocationRequest request);
        Task<BaseResponse> UnassignedFromLocationAsync(UnassignFromLocationRequest request);
    }
}
