using System.Text.Json.Serialization;

namespace Trivia.DTOs
{
    public class TriviaCategoriesResponse
    {
        [JsonPropertyName("trivia_categories")]
        public List<TriviaCategoryDTO> Trivia_Categories { get; set; }
    }

    public class TriviaCategoryDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
