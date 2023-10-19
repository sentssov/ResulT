using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static Result<TIn> Ensure<TIn>(this Result<TIn> result,
        bool condition, Error error)
    {
        if (result.IsFailure)
            return result;

        if (!condition)
            return Result.Failure<TIn>(error);

        return result;
    }
    
    public static Result<TIn> Ensure<TIn>(this Result<TIn> result,
        Func<TIn?, bool> predicate, Error error)
    {
        if (result.IsFailure)
            return result;

        return predicate(result.Value) ? result : Result.Failure<TIn>(error);
    }
}