using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApiCore.BLL.AutomapperProfiles;
using TestWebApiCore.BLL.Services;
using TestWebApiCore.Common.Models;
using TestWebApiCore.Controllers;
using TestWebApiCore.DAL;
using TestWebApiCore.DAL.Interfaces;
using Xunit;

namespace TestWebApiCore.Tests
{
    public class NotesControllerTests
    {
        private readonly AppDbContext _context;

        public NotesControllerTests()
        {
            var options = TestDataBase.CreateOptions<AppDbContext>();
            _context = new AppDbContext(options);
            _context.Database.EnsureCreated();
            AppDbContext.SeedDataBaseForUnitTest(_context);
        }

        [Fact]
        public async Task GetAllNotesAsync_ShouldReturnAllNotes()
        {
            // Arrange         
            var mockNoteMapper = new Mock<IMapper>();
            var profile = new EntityDtoProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(configuration);

            var noteRepository = new Mock<IAppRepository>();

            noteRepository.Setup(x => x.GetAllNotes())
               .ReturnsAsync(await _context.Notes.ToListAsync());

            var mockNoteControllerLogger = new Mock<ILogger<NotesController>>();

            var noteService = new NoteService(noteRepository.Object, mapper);
            var controller = new NotesController(noteService, mockNoteControllerLogger.Object);

            // Act
            var actionResult = await controller.Get();

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var notes = Assert.IsAssignableFrom<IEnumerable<NoteModel>>(objectResult.Value);

            Assert.NotNull(notes);
            Assert.NotSame("0", notes.Count().ToString());
            Assert.Equal(_context.Notes.Count().ToString(), notes.Count().ToString());
        }

        [Fact]
        public async Task GetNoteByIdAsync_ShouldReturnCorrectNoteById()
        {
            // Arrange
            int id = 1;
            var noteRepository = new Mock<IAppRepository>();
            noteRepository.Setup(x => x.GetById(id))
               .ReturnsAsync(await _context.Notes.Where(u => u.Id == id).FirstOrDefaultAsync());

            var mockNoteMapper = new Mock<IMapper>();
            var profile = new EntityDtoProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(configuration);

            var mockNoteControllerLogger = new Mock<ILogger<NotesController>>();

            var noteService = new NoteService(noteRepository.Object, mapper);
            var controller = new NotesController(noteService, mockNoteControllerLogger.Object);

            // Act
            var actionResult = await controller.Get(id);

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var note = Assert.IsType<NoteModel>(objectResult.Value);

            Assert.NotNull(note);
            Assert.Equal(id.ToString(), note.Id.ToString());
        }

        [Fact]
        public async Task GetNoteByIdAsync_ShouldNotFindNoteById()
        {
            // Arrange
            int id = 9999;
            var noteRepository = new Mock<IAppRepository>();
            noteRepository.Setup(x => x.GetById(id))
               .ReturnsAsync(await _context.Notes.Where(u => u.Id == id).FirstOrDefaultAsync());

            var mockNoteMapper = new Mock<IMapper>();
            var profile = new EntityDtoProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(configuration);

            var mockNoteControllerLogger = new Mock<ILogger<NotesController>>();

            var noteService = new NoteService(noteRepository.Object, mapper);
            var controller = new NotesController(noteService, mockNoteControllerLogger.Object);

            // Act
            var actionResult = await controller.Get(id);

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Null(objectResult.Value);

        }

        [Fact]
        public async Task DeleteNoteByIdAsync_ShouldDeleteNoteById()
        {
            // Arrange
            int id = 1;

            var noteRepository = new Mock<IAppRepository>();
            noteRepository.Setup(x => x.RemoveNote(id))
               .ReturnsAsync(await _context.Notes.Where(u => u.Id == id).AnyAsync());

            var mockNoteMapper = new Mock<IMapper>();
            var profile = new EntityDtoProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(configuration);

            var mockNoteControllerLogger = new Mock<ILogger<NotesController>>();

            var noteService = new NoteService(noteRepository.Object, mapper);
            var controller = new NotesController(noteService, mockNoteControllerLogger.Object);

            // Act
            var actionResult = await controller.Delete(id);

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            bool value = Assert.IsAssignableFrom<bool>(objectResult.Value);

            Assert.True(value);
        }
    }
}
