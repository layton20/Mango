using Mango.WEB.Adapters.Note;
using Mango.WEB.Entities.Note;
using Mango.WEB.Helpers;
using Mango.WEB.Interfaces.Managers.Note;
using Mango.WEB.Interfaces.Repositories.Note;
using Mango.WEB.Models.Base.Request;
using Mango.WEB.Models.Base.Response;
using Mango.WEB.Models.Note.Request;
using Mango.WEB.Models.Note.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.WEB.Managers.Note
{
    public class NoteManager : INoteManager
    {
        private readonly INoteRepository __NoteRepository;
        private const string ENTITY_NAME = "Note";

        public NoteManager(INoteRepository noteRepository)
        {
            __NoteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
        }

        public async Task<NoteResponse> CreateAsync(CreateNoteRequest request)
        {
            NoteEntity _CreatedEntity = await __NoteRepository.CreateAsync(request.ToEntity());

            return _CreatedEntity.ToResponse() ?? new NoteResponse
            {
                Success = false,
                ErrorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} create {ENTITY_NAME}."
            };
        }

        public async Task<BaseResponse> DeleteAsync(UIDRequest request)
        {
            BaseResponse _Response = new BaseResponse();

            if (request.UID == Guid.Empty || !await __NoteRepository.DeleteAsync(request.UID))
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} delete {ENTITY_NAME}.";
            }

            return _Response;
        }

        public async Task<NoteResponse> GetAsync(UIDRequest request)
        {
            NoteResponse _Response = new NoteResponse();

            if (request.UID == Guid.Empty)
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} retrieve {ENTITY_NAME}.";
            }

            NoteEntity _NoteEntity = await __NoteRepository.GetAsync(request.UID);

            if (_NoteEntity == null)
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} retrieve {ENTITY_NAME}.";
            }

            return _NoteEntity.ToResponse() ?? _Response;
        }

        public async Task<NotesResponse> GetAsync(BulkUIDRequest request)
        {
            IList<NoteEntity> _Entities = await __NoteRepository.GetAsync(request.UIDs ?? Enumerable.Empty<Guid>().ToList());

            return new NotesResponse
            {
                Notes = _Entities.ToResponse().ToList()
            };
        }

        public async Task<BaseResponse> UpdateAsync(UpdateNoteRequest request)
        {
            BaseResponse _Response = new BaseResponse();

            if (request.UID == Guid.Empty || !await __NoteRepository.UpdateAsync(request.ToEntity()))
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} retrieve {ENTITY_NAME}.";
            }

            return _Response;
        }
    }
}
