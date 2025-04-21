namespace TopScoreValidator.Domain.Interfaces;

public interface IValidatorService
{
    string GetLongestValidWord(string word);

    bool IsValidWord(string word);
}
