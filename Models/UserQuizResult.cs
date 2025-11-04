namespace Trivia.Models
{
    public class UserQuizResult
    {
        public int Id { get; set; }
        public string UserId { get; set; }  // Clerk User ID (string, not int)
        public int CategoryId { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public DateTime DateTaken { get; set; }

        // Navigation
        public Category Category { get; set; }
    }
}