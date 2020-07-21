using Mango.WEB.Models.Base.Request;
using Mango.WEB.Models.Base.Response;
using Mango.WEB.Models.Stock.Request;
using Mango.WEB.Models.Stock.Response;
using System.Threading.Tasks;

namespace Mango.WEB.Interfaces.Managers.Stock
{
    public interface ILocationManager
    {
        Task<LocationResponse> CreateAsync(CreateLocationRequest request);
        Task<BaseResponse> DeleteAsync(UIDRequest request);
        Task<LocationResponse> GetAsync(UIDRequest request);
        Task<LocationsResponse> GetByUserAsync(GetLocationsByUserRequest request);
        Task<BaseResponse> UpdateAsync(UpdateLocationRequest request);
    }
}
