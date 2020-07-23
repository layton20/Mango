using Mango.WEB.Entities.Stock;
using Mango.WEB.Interfaces.Repositories.Stock;
using Mango.WEB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.WEB.Repositories.Stock
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationContext __Context;

        public LocationRepository(ApplicationContext context)
        {
            __Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<LocationEntity> CreateAsync(LocationEntity location)
        {
            if (location == null || location.UID == Guid.Empty)
            {
                return null;
            }

            await __Context.Locations.AddAsync(location);
            bool _Added = await __Context.SaveChangesAsync() > 0;

            return _Added ? location : null;
        }

        public async Task<bool> DeleteAsync(Guid locationUID, Guid loggedInUserUID)
        {
            if (locationUID == Guid.Empty)
            {
                return false;
            }

            LocationEntity _Location = await __Context.Locations.FirstOrDefaultAsync(x => x.UID == locationUID);

            if (_Location == null || (_Location.UserUID != loggedInUserUID && loggedInUserUID != Guid.Empty))
            {
                return false;
            }

            __Context.Locations.Remove(_Location);

            return await __Context.SaveChangesAsync() > 0;
        }

        public async Task<LocationEntity> GetAsync(Guid locationUID)
        {
            return await __Context.Locations.FirstOrDefaultAsync(x => x.UID == locationUID);
        }

        public async Task<IList<LocationEntity>> GetByUserAsync(Guid userUID)
        {
            if (userUID == Guid.Empty)
            {
                return await __Context.Locations?.ToListAsync() ?? Enumerable.Empty<LocationEntity>().ToList();
            }

            return await __Context.Locations?.Where(x => x.UserUID == userUID).ToListAsync() ?? Enumerable.Empty<LocationEntity>().ToList();
        }

        public async Task<bool> UpdateAsync(LocationEntity updatedLocation, Guid loggedInUserUID)
        {
            if (updatedLocation.UID == Guid.Empty)
            {
                return false;
            }

            LocationEntity _LocationEntity = await __Context.Locations.FirstOrDefaultAsync(x => x.UID == updatedLocation.UID);

            // When loggedInUserUID = Guid.Empty then user has all access to modify and doesn't require comparison with location entity's UserUID field
            if (_LocationEntity == null || (_LocationEntity.UserUID != loggedInUserUID && loggedInUserUID != Guid.Empty))
            {
                return false;
            }

            _LocationEntity.Name = updatedLocation.Name;
            _LocationEntity.Description = updatedLocation.Description;
            _LocationEntity.Floor = updatedLocation.Floor;
            _LocationEntity.Address = updatedLocation.Address;
            _LocationEntity.AmendedTimestamp = DateTime.Now;

            return await __Context.SaveChangesAsync() > 0;
        }
    }
}
