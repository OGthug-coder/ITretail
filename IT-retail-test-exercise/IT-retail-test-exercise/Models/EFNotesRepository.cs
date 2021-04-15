using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace IT_retail_test_exercise.Models
{
    public class EFNotesRepository : INotesRepository
    {
        private readonly ApplicationDbContext _context;

        public EFNotesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Note> Notes => _context.Notes;

        public void SaveNote(Note note)
        {
            _context.Add(note);
            _context.SaveChanges();
        }

        public void UpdateNote(int id, Note noteIn)
        {
            var note = Notes.FirstOrDefault(n => n.Id == id);

            if (note == null)
            {
                throw new Exception("Note not found");
            }

            note.Text = noteIn.Text;

            _context.SaveChanges();
        }

        public void RemoveNote(Note note)
        {
            _context.Notes.Remove(note);
            _context.SaveChanges();
        }
    }
}