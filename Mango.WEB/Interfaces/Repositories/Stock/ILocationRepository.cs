using Mango.WEB.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mango.WEB.Interfaces.Repositories.Stock
{
    public interface ILocationRepository
    {
        Task<LocationEntity> CreateAsync(LocationEntity location);
        Task<bool> DeleteAsync(Guid locationUID, Guid loggedInUserUID);
        Task<LocationEntity> GetAsync(Guid locationUID);
        Task<IList<LocationEntity>> GetByUserAsync(Guid userUID);
        Task<bool> UpdateAsync(LocationEntity updatedLocation, Guid loggedInUserUID);
    }
}
