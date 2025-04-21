namespace TopScoreValidator.Domain.Entities;

public class ProcessWordResponse
{
    private ProcessWordResponse() { }

    public bool Success { get; private set; }

    public bool Exists { get; private set; }

    public string Valid { get; private set; }

    public string Message { get; private set; }

    public static ProcessWordResponse SuccessResponse(string word)
    {
        return new ProcessWordResponse
        {
            Success = true,
            Valid = word
        };
    }

    public static ProcessWordResponse ExistsResponse(string word)
    {
        return new ProcessWordResponse
        {
            Success = false,
            Exists = true,
            Valid = word,
            Message = $"'{word}' already exists."
        };
    }

    public static ProcessWordResponse FailureResponse(string message)
    {
        return new ProcessWordResponse
        {
            Success = false,
            Message = message
        };
    }
}
