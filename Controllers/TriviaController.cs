using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trivia.Data;

namespace Trivia.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CategoriesController : ControllerBase
{
    private readonly Services.TriviaApiService _triviaApiService;
    public CategoriesController(Services.TriviaApiService triviaApiService)
    {
        _triviaApiService = triviaApiService;
    }

    [HttpGet]

    public async Task<IActionResult> Get()
    {
        var categoriesJson = await _triviaApiService.GetCategoriesAsync();
        return Ok(categoriesJson);
    }
}