using Microsoft.EntityFrameworkCore;
using Trivia.Models;

namespace Trivia.Data
{
    public class TriviaDbContext : DbContext
    {
        public TriviaDbContext(DbContextOptions<TriviaDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }  // 👈 This tells EF Core about Categories
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}
