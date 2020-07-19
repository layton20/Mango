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
    public class DietManager : IDietManager
    {
        private readonly IDietRepository __DietRepository;
        private const string ENTITY_NAME = "Diet";

        public DietManager(IDietRepository dietRepository)
        {
            __DietRepository = dietRepository ?? throw new ArgumentNullException(nameof(dietRepository));
        }

        public async Task<DietResponse> Create(CreateDietRequest request)
        {
            DietEntity _CreatedEntity = await __DietRepository.CreateAsync(request.ToEntity());

            return _CreatedEntity.ToResponse() ?? new DietResponse
            {
                Success = false,
                ErrorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} create {ENTITY_NAME}."
            };
        }

        public async Task<BaseResponse> Delete(UIDRequest request)
        {
            BaseResponse _Response = new BaseResponse();

            if (request.UID == Guid.Empty || !await __DietRepository.DeleteAsync(request.UID))
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} delete {ENTITY_NAME}.";
            }

            return _Response;
        }

        public async Task<DietResponse> Get(UIDRequest request)
        {
            DietResponse _Response = new DietResponse();

            if (request.UID == Guid.Empty)
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} retrieve {ENTITY_NAME}.";
            }

            DietEntity _DietEntity = await __DietRepository.GetAsync(request.UID);

            if (_DietEntity == null)
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} retrieve {ENTITY_NAME}.";
            }

            return _DietEntity.ToResponse() ?? _Response;
        }

        public async Task<DietsResponse> Get(GetDietsRequest request)
        {
            IList<DietEntity> _Entities = await __DietRepository.GetAsync(request.Prefix, request.CaseSensitive) ?? new List<DietEntity>();

            return new DietsResponse
            {
                Diets = _Entities.ToResponse().ToList()
            };
        }

        public async Task<BaseResponse> Update(UpdateDietRequest request)
        {
            BaseResponse _Response = new BaseResponse();

            if (request.UID == Guid.Empty || !await __DietRepository.UpdateAsync(request.ToEntity()))
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} retrieve {ENTITY_NAME}.";
            }

            return _Response;
        }
    }
}
