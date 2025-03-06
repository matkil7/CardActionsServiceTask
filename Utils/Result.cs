namespace Utils;

public class Result<T> : Result
{
    public T? Value { get; init; } = default;

    private Result() { }

    public static new Result<T> Success(T value)
    {
        return new Result<T> { Value = value };
    }

    public static new Result<T> Failure(params string[] errors)
    {
        return new Result<T> { Errors = errors };
    }

    public static implicit operator Result<T>(T value)
    {
        return Success(value);
    }
}public class Result
{
    public bool IsSuccess => Errors == null;

    public string[] Errors { get; init; }

    protected Result() { }

    public static Result Success()
    {
        return new Result();
    }

    public static Result Failure(params string[] errors)
    {
        return new Result { Errors = errors };
    }
}