using Mango.WEB.Entities.Note;
using Mango.WEB.Models.Note.Request;
using Mango.WEB.Models.Note.Response;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mango.WEB.Adapters.Note
{
    internal static class NoteAdapter
    {
        internal static NoteEntity ToEntity(this CreateNoteRequest request)
        {
            return request == null ? null : new NoteEntity
            {
                Name = request.Name,
                Description = request.Description,
                Note = request.Note
            };
        }

        internal static NoteResponse ToResponse(this NoteEntity entity)
        {
            return entity == null ? null : new NoteResponse
            {
                Name = entity.Name,
                Description = entity.Description,
                Note = entity.Note,
                CreatedTimestamp = entity.CreatedTimestamp,
                AmendedTimestamp = entity.AmendedTimestamp
            };
        }

        internal static IEnumerable<NoteResponse> ToResponse(this IList<NoteEntity> entities)
        {
            return entities != null && entities.Count > 0 ? entities.Select(ToResponse) : Enumerable.Empty<NoteResponse>();
        }

        internal static NoteEntity ToEntity(this UpdateNoteRequest request)
        {
            return request == null ? null : new NoteEntity
            {
                UID = request.UID,
                Name = request.Name,
                Description = request.Description,
                Note = request.Note,
            };
        }
    }
}
