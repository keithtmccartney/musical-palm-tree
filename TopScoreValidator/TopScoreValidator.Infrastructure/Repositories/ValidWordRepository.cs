using Microsoft.EntityFrameworkCore;
using TopScoreValidator.Domain.Entities;
using TopScoreValidator.Domain.Interfaces;
using TopScoreValidator.Infrastructure.Data;

namespace TopScoreValidator.Infrastructure.Repositories;

public class ValidWordRepository : IValidWordRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ValidWordRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<bool> SubmitWordAsync(string word)
    {
        if (await WordExistsAsync(word))
            return false;

        var validWord = new ValidWord { Value = word };

        _applicationDbContext.ValidWords.Add(validWord);

        await _applicationDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<ValidWord>> SearchAsync(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return await _applicationDbContext.ValidWords.OrderByDescending(w => w.CreatedAt).Take(10).ToListAsync();

        return await _applicationDbContext.ValidWords.Where(w => w.Value.Contains(value)).OrderByDescending(w => w.CreatedAt).ToListAsync();
    }

    public async Task<List<ValidWord>> GetWordsAsync()
    {
        return await _applicationDbContext.ValidWords.OrderByDescending(w => w.CreatedAt).ToListAsync();
    }

    public async Task<bool> WordExistsAsync(string word)
    {
        return await _applicationDbContext.ValidWords.AnyAsync(w => w.Value == word);
    }

    public async Task<bool> DeleteWordByIdAsync(int id)
    {
        var word = await _applicationDbContext.ValidWords.FindAsync(id);

        if (word == null)
            return false;

        _applicationDbContext.ValidWords.Remove(word);

        await _applicationDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<ValidWord> GetWordByIdAsync(int id)
    {
        return await _applicationDbContext.ValidWords.FindAsync(id);
    }
}
