using System.Collections.Generic;
using System.Threading.Tasks;
using TestWebApiCore.DAL.Entities;

namespace TestWebApiCore.DAL.Interfaces
{
    public interface IAppRepository
    {
        Task<bool> Create(Note note);
        Task<IEnumerable<Note>> GetAllNotes();
        Task<bool> RemoveNote(int id);
        Task<bool> UpdateNote(int id, Note calendar);       
        Task<Note> GetById(int id);
    }
}
