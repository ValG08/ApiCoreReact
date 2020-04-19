using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestWebApiCore.BLL.IServices;
using TestWebApiCore.Common.Models;

namespace TestWebApiCore.Controllers
{
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly ILogger<NotesController> _logger;

        public NotesController
           (INoteService noteService,
           ILogger<NotesController> logger)
        {
            _noteService = noteService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteModel>>> Get()
        {
            try
            {
                IEnumerable<NoteModel> calendars = await _noteService.GetAll();
                return Ok(calendars);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NoteModel>> Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("id don't have value");
            }

            try
            {
                NoteModel note = await _noteService.GetById(id.Value);
                return Ok(note);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NoteModel noteModel)
        {
            try
            {
                return Ok(await _noteService.Create(noteModel));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] NoteModel noteModel)
        {

            try
            {
                return Ok(await _noteService.Update(id, noteModel));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
                return BadRequest(ex.Message);
            }
        }

        // DELETE:/Note/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                bool result = await _noteService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
                return BadRequest(ex.Message);
            }
        }
    }
}
