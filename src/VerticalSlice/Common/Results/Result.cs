namespace VerticalSlice.Common.Results;

public class Result
{
    public bool Failed => Error is not null;

    public Error Error { get; init; }

    protected Result()
    {   
    }

    public static Result Failure(Error error)
    {
        return new Result
        {
            Error = error
        };
    }

    public static Result Success()
    {
        return new Result();
    }
}
