using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, TOut> func)
    {
        if (result.IsFailure)
            return Result.Failure<TOut>(result.Errors);

        return Result.Create(func(result.Value));
    }
}