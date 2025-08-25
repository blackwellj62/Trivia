using System.Text.Json;
using Trivia.DTOs;

namespace Trivia.Services
{

    public class TriviaApiService
    {
        private readonly HttpClient _httpClient;

        public TriviaApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TriviaCategoryDTO>> GetCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("https://opentdb.com/api_category.php");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TriviaCategoriesResponse>(json);
            return result.Trivia_Categories;
        }
    }
}