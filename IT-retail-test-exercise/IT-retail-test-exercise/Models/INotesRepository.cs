using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace IT_retail_test_exercise.Models
{
    public interface INotesRepository
    {
        IQueryable<Note> Notes { get; }
        public void SaveNote(Note note);
        public void UpdateNote(int id, Note note);
        public void RemoveNote(Note note);
    }
}