using Mango.WEB.Entities.Note;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mango.WEB.Interfaces.Repositories.Note
{
    public interface IBookRepository
    {
        Task<BookEntity> Create(BookEntity book);
        Task<bool> Delete(Guid bookUID, bool cascade = false);
        Task<IList<BookEntity>> Get();
        Task<BookEntity> Get(Guid bookUID);
        Task<bool> Update(BookEntity book);
    }
}
