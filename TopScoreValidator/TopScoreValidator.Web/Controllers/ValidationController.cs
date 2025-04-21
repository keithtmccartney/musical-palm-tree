using Microsoft.AspNetCore.Mvc;
using TopScoreValidator.Domain.Interfaces;

namespace TopScoreValidator.Web.Controllers;

public class ValidationController : Controller
{
    private readonly ILogger<ValidationController> _logger;

    private readonly IWordService _wordService;

    public ValidationController(ILogger<ValidationController> logger, IWordService wordService)
    {
        _logger = logger;

        _wordService = wordService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Validate(string word)
    {
        var response = await _wordService.ProcessWordAsync(word);

        if (!response.Success)
        {
            ModelState.AddModelError("", response.Message);

            return View("Index");
        }

        return RedirectToAction("Index", "Submission", new { word = response.Valid });
    }
}
