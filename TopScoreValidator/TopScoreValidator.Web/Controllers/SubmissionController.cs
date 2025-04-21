using Microsoft.AspNetCore.Mvc;
using TopScoreValidator.Domain.Interfaces;
using TopScoreValidator.Web.Models;

namespace TopScoreValidator.Web.Controllers;

public class SubmissionController : Controller
{
    private readonly ILogger<SubmissionController> _logger;

    private readonly IWordService _wordService;

    public SubmissionController(ILogger<SubmissionController> logger, IWordService wordService)
    {
        _logger = logger;

        _wordService = wordService;
    }

    public IActionResult Index(string word)
    {
        if (string.IsNullOrEmpty(word))
        {
            TempData["InformationMessage"] = $"'{word}' is not valid.";

            return RedirectToAction("Index", "Validation");
        }

        var submissionViewModel = new SubmissionViewModel
        {
            Word = word,
            ShowConfirmation = true
        };

        return View("Index", submissionViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Submit(string word)
    {
        if (string.IsNullOrEmpty(word))
            return RedirectToAction("Index", "Validation");

        bool submitted = await _wordService.SubmitWordAsync(word);

        if (!submitted)
        {
            var model = new SubmissionViewModel
            {
                Word = word,
                ShowConfirmation = false,
                ShowError = true
            };

            ModelState.AddModelError("", $"'{word}' already exists.");

            return View("Index", model);
        }

        TempData["SuccessMessage"] = $"'{word}' was submitted.";

        return RedirectToAction("Index", "Validation");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var word = await _wordService.GetWordByIdAsync(id);

        if (word == null)
            return NotFound();

        return View(word);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var deleted = await _wordService.DeleteWordByIdAsync(id);

        if (deleted)
        {
            TempData["SuccessMessage"] = $"'{id}' was deleted.";
        }
        else
        {
            TempData["ErrorMessage"] = $"'{id}' was not deleted.";
        }

        return RedirectToAction("Index", "Search", new { value = TempData["Value"] });
    }
}
