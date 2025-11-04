using Microsoft.AspNetCore.Mvc;
using Trivia.Data;
using Trivia.Models;
using Microsoft.EntityFrameworkCore;

namespace Trivia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserQuizResultsController : ControllerBase
    {
        private readonly TriviaDbContext _dbContext;

        public UserQuizResultsController(TriviaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/userquizresults
        [HttpPost]
        public async Task<IActionResult> PostResult([FromBody] UserQuizResult result)
        {
            if (result == null)
                return BadRequest("Invalid quiz result data.");

            result.DateTaken = DateTime.UtcNow;
            _dbContext.UserQuizResults.Add(result);
            await _dbContext.SaveChangesAsync();

            return Ok(result);
        }

        // GET: api/userquizresults/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetResultsByUser(string userId)
        {
            var results = await _dbContext.UserQuizResults
                .Include(r => r.Category)
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.DateTaken)
                .ToListAsync();

            return Ok(results);
        }
    }
}