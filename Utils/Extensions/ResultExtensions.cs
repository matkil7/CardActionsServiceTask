namespace Utils.Extensions;

public static class ResultExtensions
{
    public static TTarget Match<TSource, TTarget>(
        this Result<TSource> result,
        Func<TSource, TTarget> onSuccess,
        Func<string[], TTarget> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Errors);
    }
}