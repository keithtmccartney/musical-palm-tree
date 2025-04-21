using TopScoreValidator.Domain.Entities;

namespace TopScoreValidator.Domain.Interfaces;

public interface IValidWordRepository
{
    Task<bool> SubmitWordAsync(string word);

    Task<List<ValidWord>> SearchAsync(string value);

    Task<List<ValidWord>> GetWordsAsync();

    Task<bool> WordExistsAsync(string word);

    Task<bool> DeleteWordByIdAsync(int id);

    Task<ValidWord> GetWordByIdAsync(int id);
}
