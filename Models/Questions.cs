namespace Trivia.Models;

public class Question
{
    public int Id { get; set; }             // Primary key
    public string Text { get; set; }        // The question itself
    public string CorrectAnswer { get; set; }
    public List<Answer> Answers { get; set; } = new List<Answer>();

    // Foreign key to Category
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}