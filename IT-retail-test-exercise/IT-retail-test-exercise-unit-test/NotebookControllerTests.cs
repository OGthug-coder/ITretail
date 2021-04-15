using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using IT_retail_test_exercise.Controllers;
using IT_retail_test_exercise.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace IT_retail_test_exercise_unit_test
{
    public class NotebookControllerTests
    {
        [Fact]
        public void Can_Get_Notes()
        {
            var mock = Arrange();
            NotebookController controller = new NotebookController(mock.Object);
    
            ActionResult<List<Note>> result = 
                controller.GetNotes(new UserRequest {Token = "token0"});
            
            Assert.Equal(3, result.Value.Count);
            Assert.Equal("N1", result.Value[0].Text);
            Assert.Equal("N3", result.Value[1].Text);
            Assert.Equal("N5", result.Value[2].Text);
        }

        [Fact]
        public void Can_Get_Note()
        {
            var mock = Arrange();
            NotebookController controller = new NotebookController(mock.Object);

            var result = controller.GetNote(3);
            
            Assert.Equal(3, result.Value.Id);
            Assert.Equal("N3", result.Value.Text);
        }

        [Fact]
        public void Can_Create_Note()
        {
            var mock = Arrange();
            var controller = new NotebookController(mock.Object);
            var note = new Note {Id = 6, Text = "N6", Important = false, UserToken = "token1"};
            
            Note result = controller.CreateNote(note).Value;

            Assert.Equal("N6", result.Text);
        }

        [Fact]
        public void Can_Remove_Note()
        {
            var mock = Arrange();
            var controller = new NotebookController(mock.Object);

            var result = controller.DeleteNote(1);
            
            Assert.IsType<NoContentResult>(result);
        }

        private Mock<INotesRepository> Arrange()
        {
            Mock<INotesRepository> mock = new Mock<INotesRepository>();
            mock.Setup(m => m.Notes).Returns((new Note[]
            {
                new Note {Id = 1, Text = "N1", Important = true, UserToken = "token0"},
                new Note {Id = 2, Text = "N2", Important = false, UserToken = "token1"},
                new Note {Id = 3, Text = "N3", Important = true, UserToken = "token0"},
                new Note {Id = 4, Text = "N4", Important = false, UserToken = "token1"},
                new Note {Id = 5, Text = "N5", Important = true, UserToken = "token0"},
            }).AsQueryable<Note>());

            return mock;
        }
    }
}