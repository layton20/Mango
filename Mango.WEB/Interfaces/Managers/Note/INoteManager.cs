using Mango.WEB.Models.Base.Request;
using Mango.WEB.Models.Base.Response;
using Mango.WEB.Models.Note.Request;
using Mango.WEB.Models.Note.Response;
using System.Threading.Tasks;

namespace Mango.WEB.Interfaces.Managers.Note
{
    public interface INoteManager
    {
        Task<NoteResponse> CreateAsync(CreateNoteRequest request);
        Task<BaseResponse> DeleteAsync(UIDRequest request);
        Task<NoteResponse> GetAsync(UIDRequest request);
        Task<NotesResponse> GetAsync(BulkUIDRequest request);
        Task<BaseResponse> UpdateAsync(UpdateNoteRequest request);
    }
}
