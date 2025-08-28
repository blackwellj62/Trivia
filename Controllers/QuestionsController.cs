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

         // ✅ 1. GET: Return all questions (with answers) from DB
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var questions = await _dbContext.Questions
                .Include(q => q.Answers) // eager load answers
                .Include(q => q.Category)
                .ToListAsync();

            return Ok(questions);
        }

        // ✅ 2. POST: Fetch questions from API and save into DB
        [HttpPost("{categoryId}")]
        public async Task<IActionResult> FetchAndSaveQuestions(int categoryId)
        {
            // Step 1: Pull from OpenTDB
            var questionsFromApi = await _triviaApi.GetQuestionsAsync(categoryId);

            if (questionsFromApi == null || !questionsFromApi.Any())
            {
                return BadRequest("No questions returned from API.");
            }

            // Step 2: Map DTO → Entity
            var questionEntities = questionsFromApi.Select(q => new Question
            {
                Text = System.Net.WebUtility.HtmlDecode(q.Question), 
                CorrectAnswer = System.Net.WebUtility.HtmlDecode(q.Correct_Answer),
                CategoryId = categoryId,
                Answers = q.Incorrect_Answers
                    .Select(a => new Answer { Text = System.Net.WebUtility.HtmlDecode(a) })
                    .ToList()
            }).ToList();

            // Step 3: Save to DB
            _dbContext.Questions.AddRange(questionEntities);
            await _dbContext.SaveChangesAsync();

            return Ok(questionEntities);
        }
    }
}

       

