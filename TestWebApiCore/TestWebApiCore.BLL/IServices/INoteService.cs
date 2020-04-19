using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestWebApiCore.Common.Models;

namespace TestWebApiCore.BLL.IServices
{
    public interface INoteService
    {
        Task<IEnumerable<NoteModel>> GetAll();
        Task<bool> Create(NoteModel noteModel);
        Task<bool> Delete(int id);
        Task<bool> Update(int id, NoteModel noteModel);
        Task<NoteModel> GetById(int id);
    }
}
