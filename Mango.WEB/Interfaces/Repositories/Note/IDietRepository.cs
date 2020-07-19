using Mango.WEB.Entities.Note;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mango.WEB.Interfaces.Repositories.Note
{
    public interface IDietRepository
    {
        Task<DietEntity> CreateAsync(DietEntity diet);
        Task<bool> DeleteAsync(Guid dietUID);
        Task<DietEntity> GetAsync(Guid dietUID);
        Task<IList<DietEntity>> GetAsync(string prefix = "", bool caseSensitive = false);
        Task<bool> UpdateAsync(DietEntity updatedDiet);
    }
}
