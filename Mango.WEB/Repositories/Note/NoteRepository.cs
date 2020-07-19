using Mango.WEB.Entities.Note;
using Mango.WEB.Interfaces.Repositories.Note;
using Mango.WEB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.WEB.Repositories.Note
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationContext __Context;

        public NoteRepository(ApplicationContext context)
        {
            __Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<NoteEntity> CreateAsync(NoteEntity note)
        {
            if (note == null || note.UID == Guid.Empty)
            {
                return null;
            }

            await __Context.Notes.AddAsync(note);
            bool _Added = await __Context.SaveChangesAsync() > 0;

            return _Added ? note : null;
        }

        public async Task<bool> DeleteAsync(Guid noteUID)
        {
            if (noteUID == Guid.Empty)
            {
                return false;
            }

            NoteEntity _Diet = await __Context.Notes.FirstOrDefaultAsync(x => x.UID == noteUID);

            if (_Diet == null)
            {
                return false;
            }

            __Context.Notes.Remove(_Diet);

            return await __Context.SaveChangesAsync() > 0;
        }

        public async Task<NoteEntity> GetAsync(Guid noteUID)
        {
            return await __Context.Notes.FirstOrDefaultAsync(x => x.UID == noteUID);
        }

        public async Task<IList<NoteEntity>> GetAsync(IList<Guid> noteUIDs)
        {
            return await __Context.Notes.Where(x => noteUIDs.Any(uid => uid == x.UID)).ToListAsync() ?? Enumerable.Empty<NoteEntity>().ToList();
        }

        public async Task<bool> UpdateAsync(NoteEntity updatedNote)
        {
            if (updatedNote.UID == Guid.Empty)
            {
                return false;
            }

            NoteEntity _NoteEntity = await __Context.Notes.FirstOrDefaultAsync(x => x.UID == updatedNote.UID);

            if (_NoteEntity == null)
            {
                return false;
            }

            _NoteEntity = updatedNote;
            return await __Context.SaveChangesAsync() > 0;
        }
    }
}
