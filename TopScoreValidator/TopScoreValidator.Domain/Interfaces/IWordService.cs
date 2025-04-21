using TopScoreValidator.Domain.Entities;

namespace TopScoreValidator.Domain.Interfaces;

public interface IWordService
{
    Task<ProcessWordResponse> ProcessWordAsync(string word);

    Task<bool> SubmitWordAsync(string word);

    Task<List<ValidWord>> SearchAsync(string value);

    Task<Metrics> GetMetricsAsync();

    Task<bool> DeleteWordByIdAsync(int id);

    Task<ValidWord> GetWordByIdAsync(int id);

    Task<List<ValidWord>> GetRecentByCountAsync(int count);
}
