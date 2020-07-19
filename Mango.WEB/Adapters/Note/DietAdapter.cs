using Mango.WEB.Entities.Note;
using Mango.WEB.Models.Note.Request;
using Mango.WEB.Models.Note.Response;
using System.Collections.Generic;
using System.Linq;

namespace Mango.WEB.Adapters.Note
{
    internal static class DietAdapter
    {
        internal static DietEntity ToEntity(this CreateDietRequest request)
        {
            return request == null ? null : new DietEntity
            {
                Name = request.Name,
                Description = request.Description
            };
        }

        internal static DietResponse ToResponse(this DietEntity entity)
        {
            return entity == null ? null : new DietResponse
            {
                Name = entity.Name,
                Description = entity.Description
            };
        }

        internal static IEnumerable<DietResponse> ToResponse(this IList<DietEntity> entities)
        {
            return entities != null && entities.Count > 0 ? entities.Select(ToResponse) : Enumerable.Empty<DietResponse>();
        }

        internal static DietEntity ToEntity(this UpdateDietRequest request)
        {
            return request == null ? null : new DietEntity
            {
                UID = request.UID,
                Name = request.Name,
                Description = request.Description
            };
        }
    }
}
