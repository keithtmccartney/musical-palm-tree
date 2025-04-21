using TopScoreValidator.Domain.Interfaces;

namespace TopScoreValidator.Application.Services;

public class ValidatorService : IValidatorService
{
    public string GetLongestValidWord(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            return null;

        var words = word.Split(' ').Where(w => !string.IsNullOrWhiteSpace(w)).ToList();

        var validWords = new List<string>();

        foreach (var w in words)
        {
            if (IsValidWord(w))
                validWords.Add(w);
        }

        return validWords.OrderByDescending(w => w.Length).FirstOrDefault();
    }

    public bool IsValidWord(string word)
    {
        if (string.IsNullOrWhiteSpace(word) || word.Length < 8)
            return false;

        bool uppercase = false;

        bool lowercase = false;

        bool numerical = false;

        HashSet<char> uniqueCharacters = new HashSet<char>();

        foreach (char c in word)
        {
            if (!uniqueCharacters.Add(c))
                return false;

            if (char.IsUpper(c))
                uppercase = true;
            else if (char.IsLower(c))
                lowercase = true;
            else if (char.IsDigit(c))
                numerical = true;
        }

        return uppercase && lowercase && numerical;
    }
}
