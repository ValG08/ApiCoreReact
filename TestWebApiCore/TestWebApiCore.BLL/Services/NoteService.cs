using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestWebApiCore.BLL.IServices;
using TestWebApiCore.Common.Models;
using TestWebApiCore.DAL.Entities;
using TestWebApiCore.DAL.Interfaces;

namespace TestWebApiCore.BLL.Services
{
    public class NoteService : INoteService
    {
        private readonly IAppRepository _repository;
        private readonly IMapper _mapper;

        public NoteService(IAppRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Create(NoteModel noteModel)
        {
            try
            {
                Note mapper = _mapper.Map<Note>(noteModel);
                return _mapper.Map<bool>(await _repository.Create(mapper));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                return _mapper.Map<bool>(await _repository.RemoveNote(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<NoteModel>> GetAll()
        {
            try
            {
                IEnumerable<Note> calendars = await _repository.GetAllNotes();

                return _mapper.Map<IEnumerable<NoteModel>>(calendars);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<NoteModel> GetById(int id)
        {
            try
            {
                Note note = await _repository.GetById(id);
                return _mapper.Map<NoteModel>(note);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }   

        public async Task<bool> Update(int id, NoteModel noteModel)
        {
            try
            {
                var noteMap = _mapper.Map<Note>(noteModel);
                return await _repository.UpdateNote(id, noteMap);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
