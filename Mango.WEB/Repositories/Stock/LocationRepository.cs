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

        public async Task<bool> DeleteAsync(Guid locationUID)
        {
            if (locationUID == Guid.Empty)
            {
                return false;
            }

            LocationEntity _Location = await __Context.Locations.FirstOrDefaultAsync(x => x.UID == locationUID);

            if (_Location == null)
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

        public async Task<bool> UpdateAsync(LocationEntity updatedLocation)
        {
            if (updatedLocation.UID == Guid.Empty)
            {
                return false;
            }

            LocationEntity _LocationEntity = await __Context.Locations.FirstOrDefaultAsync(x => x.UID == updatedLocation.UID);

            if (_LocationEntity == null)
            {
                return false;
            }

            _LocationEntity = updatedLocation;
            return await __Context.SaveChangesAsync() > 0;
        }
    }
}
