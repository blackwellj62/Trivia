namespace Trivia.DTOs;

using System.Text.Json.Serialization;



    public class TriviaApiResponseDTO
    {
        [JsonPropertyName("response_code")]
        public int Response_Code { get; set; }

        [JsonPropertyName("results")]
        public List<TriviaQuestionDTO> Results { get; set; } = new();
    }

    public class TriviaQuestionDTO
    {
        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("difficulty")]
        public string Difficulty { get; set; }

        [JsonPropertyName("question")]
        public string Question { get; set; }

        [JsonPropertyName("correct_answer")]
        public string Correct_Answer { get; set; }

        [JsonPropertyName("incorrect_answers")]
        public List<string> Incorrect_Answers { get; set; } = new();
    }
