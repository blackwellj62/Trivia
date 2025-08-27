using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trivia.Data;
using Trivia.Models;
using Trivia.Services;
using Trivia.DTOs;

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


        [HttpPost("{questionId}/check")]
        public async Task<IActionResult> CheckAnswer(int questionId, [FromBody] CheckAnswerRequest request)
        {
            // 1) Basic request validation
            if (request == null)
                return BadRequest("Request body required.");
            if (request.AnswerId <= 0)
                return BadRequest("A valid AnswerId is required.");

            // 2) Load the question and its answers from the DB (eager-load answers)
            var question = await _dbContext.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == questionId);

            if (question == null)
                return NotFound($"Question with id {questionId} not found.");

            // 3) Find the chosen answer among the loaded question answers
            var chosen = question.Answers.FirstOrDefault(a => a.Id == request.AnswerId);
            if (chosen == null)
            {
                // either answer doesn't exist, or it doesn't belong to this question
                return BadRequest("The selected answer does not belong to the requested question.");
            }

            // 4) Determine correctness
            // We compare the chosen answer's text to the question's stored CorrectAnswer.
            // Use trim + ordinal ignore-case to avoid false negatives due to whitespace/casing.
            var isCorrect = string.Equals(
                chosen.Text?.Trim(),
                question.CorrectAnswer?.Trim(),
                StringComparison.OrdinalIgnoreCase);

            // 5) (Optional) Persist attempt / increment statistics, etc. - omitted for now

            // 6) Return a small result object the frontend can use
            return Ok(new
            {
                questionId = question.Id,
                selectedAnswerId = chosen.Id,
                isCorrect = isCorrect
            });
        }
    }
}
