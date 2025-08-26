using System.Text.Json;
using System.Net.Http.Json;
using Trivia.DTOs;
using System.Net;

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

       public async Task<List<TriviaQuestionDTO>> GetQuestionsAsync(
            int categoryId,
            int amount = 10,
            string? difficulty = null)
        {
            // 1) Build the relative URL with query params
            var url = $"api.php?amount={amount}&category={categoryId}&type=multiple";
            if (!string.IsNullOrWhiteSpace(difficulty))
            {
                // If you later allow user-entered difficulty, encode it to be safe.
                url += $"&difficulty={Uri.EscapeDataString(difficulty)}";
            }

            // 2) Make the HTTP request and deserialize JSON in one step
            var api = await _httpClient.GetFromJsonAsync<TriviaApiResponseDTO>(url);

            // 3) Defensive checks: handle nulls or API error codes
            if (api == null || api.Results == null || api.Results.Count == 0 || api.Response_Code != 0)
            {
                // Returning an empty list lets the controller decide what to do (e.g., 404)
                return new List<TriviaQuestionDTO>();
            }

            // 4) Normalize/clean the data *before* the rest of the app sees it
            //    OpenTDB escapes special characters; decode them here.
            foreach (var q in api.Results)
            {
                q.Question = WebUtility.HtmlDecode(q.Question);
                q.Correct_Answer = WebUtility.HtmlDecode(q.Correct_Answer);

                for (int i = 0; i < q.Incorrect_Answers.Count; i++)
                {
                    q.Incorrect_Answers[i] = WebUtility.HtmlDecode(q.Incorrect_Answers[i]);
                }
            }

            // 5) Return clean DTOs to the controller
            return api.Results;
        }
    }
}