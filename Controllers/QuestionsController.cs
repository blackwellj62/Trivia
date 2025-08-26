using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trivia.Data;
using Trivia.Models;
using Trivia.Services;

namespace Trivia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly TriviaDbContext _dbContext;
        private readonly TriviaApiService _triviaApi;

        public QuestionsController(TriviaDbContext dbContext, TriviaApiService triviaApi)
        {
            _dbContext = dbContext;
            _triviaApi = triviaApi;
        }

        // GET /api/questions?categoryId=9&amount=10&difficulty=easy
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] int categoryId,
            [FromQuery] int amount = 10,
            [FromQuery] string? difficulty = null)
        {
            // 1) Validate inputs (guard rails help avoid weird API calls)
            if (categoryId <= 0)
            {
                return BadRequest("categoryId must be a positive integer.");
            }
            if (amount <= 0 || amount > 50)
            {
                // OpenTDB supports up to ~50; keep a reasonable cap.
                amount = 10;
            }

            // 2) Ask the service for questions (already cleaned/decoded)
            var apiQuestions = await _triviaApi.GetQuestionsAsync(categoryId, amount, difficulty);

            if (apiQuestions.Count == 0)
            {
                // Let the client know the upstream gave us nothing usable
                return NotFound("No questions available from provider for the given parameters.");
            }

            // 3) Map external DTOs → internal EF entities
            //    We combine incorrect+correct answers into a single list of Answer entities.
            //    We DO NOT expose which one is correct in the response body (we’ll add a /check endpoint later).
            var newQuestions = apiQuestions.Select(q => new Question
            {
                Text = q.Question,
                CorrectAnswer = q.Correct_Answer,  // stored in DB, not exposed in the GET response
                CategoryId = categoryId,
                Answers = q.Incorrect_Answers
                           .Append(q.Correct_Answer)   // put all choices together
                           .Select(a => new Answer { Text = a })
                           .OrderBy(_ => Guid.NewGuid()) // simple shuffle so correct isn’t always last
                           .ToList()
            }).ToList();

            // 4) Persist to DB (so you can reuse, analyze, or show leaderboards later)
            _dbContext.Questions.AddRange(newQuestions);
            await _dbContext.SaveChangesAsync();

            // 5) Shape a "safe" response for the frontend (no CorrectAnswer leaked)
            //    Return just what the game needs to render: question text + answer choices.
            //    NOTE: Using a typed response DTO is better long-term; an anonymous shape is fine to start.
            var response = newQuestions.Select(q => new
            {
                id = q.Id,                 // now populated after SaveChanges
                text = q.Text,
                categoryId = q.CategoryId,
                answers = q.Answers.Select(a => new { id = a.Id, text = a.Text })
            });

            return Ok(response);
        }
    }
}
