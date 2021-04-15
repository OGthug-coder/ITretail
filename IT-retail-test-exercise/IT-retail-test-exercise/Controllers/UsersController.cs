using System.Threading.Tasks;
using IT_retail_test_exercise.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace IT_retail_test_exercise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<User>> Register()
        {
            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            User user = new User {Token = token};

            _context.Users.Add(user);
            
            await _context.SaveChangesAsync();

            return user;
        }
    }
}