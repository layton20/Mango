using Mango.WEB.Models.Base.Request;
using Mango.WEB.Models.Base.Response;
using Mango.WEB.Models.Note.Request;
using Mango.WEB.Models.Note.Response;
using System.Threading.Tasks;

namespace Mango.WEB.Interfaces.Managers.Note
{
    public interface IDietManager
    {
        Task<DietResponse> Create(CreateDietRequest request);
        Task<BaseResponse> Delete(UIDRequest request);
        Task<DietResponse> Get(UIDRequest request);
        Task<DietsResponse> Get(GetDietsRequest request);
        Task<BaseResponse> Update(UpdateDietRequest request);
    }
}
