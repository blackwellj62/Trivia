public class TriviaApiService
{
    private readonly HttpClient _httpClient;

    public TriviaApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetCategoriesAsync()
    {
        var response = await _httpClient.GetAsync("https://opentdb.com/api_category.php");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}