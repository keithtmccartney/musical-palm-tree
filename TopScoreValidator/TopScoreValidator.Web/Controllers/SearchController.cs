using Microsoft.AspNetCore.Mvc;
using TopScoreValidator.Domain.Entities;
using TopScoreValidator.Domain.Interfaces;
using TopScoreValidator.Web.Models;

namespace TopScoreValidator.Web.Controllers;

public class SearchController : Controller
{
    private readonly ILogger<SearchController> _logger;

    private readonly IWordService _wordService;

    public SearchController(ILogger<SearchController> logger, IWordService wordService)
    {
        _logger = logger;

        _wordService = wordService;
    }

    public async Task<IActionResult> Index(string value)
    {
        value = string.IsNullOrWhiteSpace(value) ? null : value.Trim();

        bool isSearching = value != null;

        List<ValidWord> words = isSearching ? await _wordService.SearchAsync(value) : await _wordService.GetRecentByCountAsync(10);

        var stats = await _wordService.GetMetricsAsync();

        var model = new WordSearchViewModel
        {
            Words = words,
            Metrics = stats,
            Value = value,
            IsSearching = isSearching
        };

        return View(model);
    }
}
