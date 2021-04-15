using Microsoft.EntityFrameworkCore;

namespace IT_retail_test_exercise.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options){}

        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}