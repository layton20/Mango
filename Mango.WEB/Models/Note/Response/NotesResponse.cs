using Mango.WEB.Models.Base.Response;
using System.Collections.Generic;

namespace Mango.WEB.Models.Note.Response
{
    public class NotesResponse : BaseResponse
    {
        public IList<NoteResponse> Notes { get; set; }
    }
}
