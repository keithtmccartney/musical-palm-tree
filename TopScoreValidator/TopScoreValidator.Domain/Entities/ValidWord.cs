namespace TopScoreValidator.Domain.Entities;

public class ValidWord
{
    public int Id { get; set; }

    public string Value { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
