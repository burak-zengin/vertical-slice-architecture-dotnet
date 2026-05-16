namespace VerticalSlice.Common.Results;

public class Result<T> : Result
{
    public T Data { get; set; }

    protected Result()
    {   
    }

    public static Result<T> Failure(Error error)
    {
        return new Result<T>
        {
            Error = error
        };
    }

    public static Result<T> Success(T data)
    {
        return new Result<T>
        {
            Data = data
        };
    }
}
