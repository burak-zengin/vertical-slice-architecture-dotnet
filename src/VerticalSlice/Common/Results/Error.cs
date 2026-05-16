namespace VerticalSlice.Common.Results;

public class Error(string code,string message)
{
    public string Code { get; init; } = code;

    public string Message { get; init; } = message;
}
