namespace Trivia.DTOs
{
    public class TriviaCategoriesResponse
    {
        public List<TriviaCategoryDTO> Trivia_Categories { get; set; }
    }

    public class TriviaCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}