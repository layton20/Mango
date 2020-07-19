using Mango.WEB.Entities.Note;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mango.WEB.Interfaces.Repositories.Note
{
    public interface IBookNoteRepository
    {
        Task<IList<NoteEntity>> GetNotesAsync(Guid bookUID);
        Task<IList<BookEntity>> GetBooksAsync(Guid noteUID);
        Task<bool> AddNoteToBookAsync(Guid noteUID, Guid bookUID);
        Task<bool> AddNotesToBookAsync(IList<Guid> noteUIDs, Guid bookUID);
        Task<bool> DeleteNoteFromBookAsync(Guid noteUID, Guid bookUID);
        Task<bool> DeleteNotesFromBookAsync(IList<Guid> noteUIDs, Guid bookUID);
    }
}
