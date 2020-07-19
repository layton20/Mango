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
    public class DietRepository : IDietRepository
    {
        private readonly ApplicationContext __Context;

        public DietRepository(ApplicationContext context)
        {
            __Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<DietEntity> CreateAsync(DietEntity diet)
        {
            if (diet == null || diet.UID == Guid.Empty)
            {
                return null;
            }

            await __Context.Diets.AddAsync(diet);
            bool _Added = await __Context.SaveChangesAsync() > 0;

            return _Added ? diet : null;
        }

        public async Task<bool> DeleteAsync(Guid dietUID)
        {
            if (dietUID == Guid.Empty)
            {
                return false;
            }

            DietEntity _Diet = await __Context.Diets.FirstOrDefaultAsync(x => x.UID == dietUID);

            if (_Diet == null)
            {
                return false;
            }

            __Context.Diets.Remove(_Diet);

            return await __Context.SaveChangesAsync() > 0;
        }

        public async Task<DietEntity> GetAsync(Guid dietUID)
        {
            return await __Context.Diets.FirstOrDefaultAsync(x => x.UID == dietUID);
        }

        public async Task<IList<DietEntity>> GetAsync(string prefix = "", bool caseSensitive = false)
        {
            return await __Context.Diets.Where(x => x.Name.StartsWith(prefix, caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))?.ToListAsync()
                ?? Enumerable.Empty<DietEntity>().ToList();
        }

        public async Task<bool> UpdateAsync(DietEntity updatedDiet)
        {
            if (updatedDiet.UID == Guid.Empty)
            {
                return false;
            }

            DietEntity _DietEntity = await __Context.Diets.FirstOrDefaultAsync(x => x.UID == updatedDiet.UID);

            if (_DietEntity == null)
            {
                return false;
            }

            _DietEntity = updatedDiet;
            return await __Context.SaveChangesAsync() > 0;
        }
    }
}
