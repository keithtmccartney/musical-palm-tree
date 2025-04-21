namespace TopScoreValidator.Domain.Entities;

public class Metrics
{
    public int TotalWordCount { get; set; }

    public double AverageWordLength { get; set; }

    public string LongestWord { get; set; }

    public string MostRecentWord { get; set; }
}
