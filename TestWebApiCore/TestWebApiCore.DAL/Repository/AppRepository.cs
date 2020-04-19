using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApiCore.DAL.Entities;
using TestWebApiCore.DAL.Interfaces;

namespace TestWebApiCore.DAL.Repository
{
    public class AppRepository : IAppRepository
    {
        private readonly AppDbContext _context;

        public AppRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Note note)
        {
            if (_context == null)
            {
                throw new Exception("DbContext is null");
            }

            try
            {
                if (note == null || string.IsNullOrEmpty(note.NoteMessage))
                {
                    return false;
                }

                await _context.Notes.AddAsync(note);
                return Convert.ToBoolean(await _context.SaveChangesAsync());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Note>> GetAllNotes()
        {
            try
            {
                return await QueryableAllNotes().ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Note> GetById(int id)
        {
            if (_context == null)
            {
                throw new Exception("DbContext is null");
            }

            try
            {
                return await QueryableAllNotes().Where(u => u.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveNote(int id)
        {
            if (_context == null)
            {
                throw new Exception("DbContext is null");
            }

            try
            {
                _context.Remove(_context.Notes.Single(a => a.Id == id));
                return Convert.ToBoolean(await _context.SaveChangesAsync());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateNote(int id, Note calendar)
        {
            if (_context == null)
            {
                throw new Exception("DbContext is null");
            }

            try
            {
                var noteEntity = QueryableAllNotes().Where(u => u.Id == id).FirstOrDefault();
                if (noteEntity != null)
                {
                    noteEntity.NoteMessage = calendar.NoteMessage;
                }

                return Convert.ToBoolean(await _context.SaveChangesAsync());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private IQueryable<Note> QueryableAllNotes()
        {
            if (_context == null)
            {
                throw new Exception("DbContext is null");
            }

            try
            {
                return _context.Notes.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
