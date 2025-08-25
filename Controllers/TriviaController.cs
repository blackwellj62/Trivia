using Microsoft.AspNetCore.Mvc;
using Trivia.Data;
using Trivia.Models;
using Trivia.Services;
using Trivia.DTOs;

namespace Trivia.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CategoriesController : ControllerBase
{
    private readonly TriviaDbContext _dbContext;
    private readonly TriviaApiService _triviaApiService;
    public CategoriesController(TriviaDbContext context, TriviaApiService triviaApiService)
    {
        _dbContext = context;
        _triviaApiService = triviaApiService;
    }

    [HttpGet]

    public async Task<IActionResult> Get()
        {
            // If DB already has categories, return them
            if (_dbContext.Categories.Any())
            {
                return Ok(_dbContext.Categories.ToList());
            }

            // Otherwise fetch from API
            var categoriesFromApi = await _triviaApiService.GetCategoriesAsync();
        if (categoriesFromApi == null || !categoriesFromApi.Any())
        {
            return StatusCode(500, "Unable to fetch categories from API");
        }


            // Map DTOs to EF entities
        var categories = categoriesFromApi.Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            // Save into DB
            _dbContext.Categories.AddRange(categories);
            await _dbContext.SaveChangesAsync();

            return Ok(categories);
        }
    }
