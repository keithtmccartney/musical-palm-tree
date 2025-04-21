namespace TopScoreValidator.Web.Models;

public class SubmissionViewModel
{
    public string Word { get; set; }

    public bool ShowConfirmation { get; set; }

    public bool ShowError { get; set; }

    public bool ConfirmSubmission { get; set; }
}
