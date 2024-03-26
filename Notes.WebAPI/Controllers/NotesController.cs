using Microsoft.AspNetCore.Mvc;
using Notes.BLL.Interfaces;
using Notes.BLL.Models;
using Notes.WebAPI.Validation;

namespace Notes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteModel>>> Get()
        {
            var models = await _noteService.GetAllAsync();

            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NoteModel>> GetById(Guid id)
        {
            var model = await _noteService.GetByIdAsync(id);

            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Add([FromBody] NoteModel model)
        {
            var valid = NoteValidator.Validation(model);

            if(valid != string.Empty)
            {
                return BadRequest(valid);
            }

            var id = await _noteService.AddAsync(model);

            return Ok(id);
        }

        [HttpPut]
        public async Task<ActionResult<NoteModel>> Update([FromBody] NoteModel model)
        {
            var valid = NoteValidator.Validation(model);

            if (valid != string.Empty)
            {
                return BadRequest(valid);
            }

            var updatedModel = await _noteService.UpdateAsync(model);

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _noteService.DeleteAsync(id);

            return Ok();
        }
    }
}
