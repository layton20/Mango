using Mango.WEB.Entities.Note;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mango.WEB.Interfaces.Repositories.Note
{
    public interface INoteRepository
    {
        Task<NoteEntity> CreateAsync(NoteEntity note);
        Task<bool> DeleteAsync(Guid noteUID);
        Task<NoteEntity> GetAsync(Guid noteUID);
        Task<IList<NoteEntity>> GetAsync(IList<Guid> noteUIDs);
        Task<bool> UpdateAsync(NoteEntity updatedNote);
    }
}
