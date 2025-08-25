using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// using Trivia.Models;
using Microsoft.AspNetCore.Identity;

namespace Trivia.Data;
public class TriviaDbContext : IdentityDbContext<IdentityUser>
{
    private readonly IConfiguration _configuration;
    
    

    public TriviaDbContext(DbContextOptions<TriviaDbContext> context, IConfiguration config) : base(context)
    {
        _configuration = config;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
    }
}