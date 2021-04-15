using System;
using IT_retail_test_exercise.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IT_retail_test_exercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotebookController : ControllerBase
    {
        private readonly INotesRepository _repository;

        public NotebookController(INotesRepository repository)
        {
            _repository = repository;
        }
        
        [HttpPost]
        [Route("Notes")]
        public ActionResult<List<Note>> GetNotes(UserRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            
            var userToken = request.Token;

            return _repository.Notes.Where(x => x.UserToken == userToken).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Note> GetNote(int id)
        {
            var note = _repository.Notes.FirstOrDefault(n => n.Id == id);

            if (note == null)
            {
                return NotFound();
            }
            
            return note;
        }
        
        [HttpPost]
        public ActionResult<Note> CreateNote(Note note)
        {
            _repository.SaveNote(note);

            return note;
        }
        
        [HttpPut("{id}")]
        public ActionResult<Note> UpdateNote(int id, Note noteIn)
        {
            if (id != noteIn.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.UpdateNote(id, noteIn);
            }
            catch (Exception) when(!NoteExists(id))
            {
                return NotFound();
            }
        
            return RedirectToAction(nameof(GetNote), new {id = id});
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            var note = _repository.Notes.FirstOrDefault(n => n.Id == id);
        
            if (note == null)
            {
                return NotFound();
            }
        
            _repository.RemoveNote(note);
        
            return NoContent();
        } 

        private bool NoteExists(int id) =>
            _repository.Notes.Any(e => e.Id == id);
    }
}