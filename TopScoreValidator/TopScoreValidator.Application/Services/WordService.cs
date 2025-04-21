using TopScoreValidator.Domain.Entities;
using TopScoreValidator.Domain.Interfaces;

namespace TopScoreValidator.Application.Services;

public class WordService : IWordService
{
    private readonly IValidWordRepository _validWordRepository;

    private readonly IValidatorService _validatorService;

    public WordService(IValidWordRepository validWordRepository, IValidatorService validatorService)
    {
        _validWordRepository = validWordRepository;

        _validatorService = validatorService;
    }

    public async Task<ProcessWordResponse> ProcessWordAsync(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            return ProcessWordResponse.FailureResponse("Please enter a valid word.");

        var longestValidWord = _validatorService.GetLongestValidWord(word);

        if (string.IsNullOrEmpty(longestValidWord))
            return ProcessWordResponse.FailureResponse("No valid word was found.");

        bool exists = await _validWordRepository.WordExistsAsync(longestValidWord);

        if (exists)
            return ProcessWordResponse.ExistsResponse(longestValidWord);

        return ProcessWordResponse.SuccessResponse(longestValidWord);
    }

    public async Task<bool> SubmitWordAsync(string word)
    {
        if (string.IsNullOrWhiteSpace(word) || !_validatorService.IsValidWord(word))
            return false;

        return await _validWordRepository.SubmitWordAsync(word);
    }

    public async Task<List<ValidWord>> SearchAsync(string value)
    {
        return await _validWordRepository.SearchAsync(value);
    }

    public async Task<Metrics> GetMetricsAsync()
    {
        var words = await _validWordRepository.GetWordsAsync();

        return new Metrics
        {
            TotalWordCount = words.Count,
            AverageWordLength = words.Count > 0 ? Math.Round(words.Average(w => w.Value.Length)) : 0,
            LongestWord = words.OrderByDescending(w => w.Value.Length).FirstOrDefault()?.Value,
            MostRecentWord = words.OrderByDescending(w => w.CreatedAt).FirstOrDefault()?.Value
        };
    }

    public async Task<bool> DeleteWordByIdAsync(int id)
    {
        return await _validWordRepository.DeleteWordByIdAsync(id);
    }

    public async Task<ValidWord> GetWordByIdAsync(int id)
    {
        return await _validWordRepository.GetWordByIdAsync(id);
    }

    public async Task<List<ValidWord>> GetRecentByCountAsync(int count)
    {
        var words = await _validWordRepository.GetWordsAsync();

        return words.OrderByDescending(w => w.CreatedAt).Take(count).ToList();
    }
}
