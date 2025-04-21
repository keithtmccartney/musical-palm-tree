using TopScoreValidator.Domain.Entities;

namespace TopScoreValidator.Web.Models;

public class WordSearchViewModel
{
    public List<ValidWord> Words { get; set; } = new List<ValidWord>();

    public Metrics Metrics { get; set; } = new Metrics();

    public string Value { get; set; }

    public bool IsSearching { get; set; }

    public int Count => Words?.Count ?? 0;
}
