using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<Result<TOut>> Map<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, Task<TOut>> func)
    {
        if (result.IsFailure)
            return Result.Failure<TOut>(result.Errors);

        return Result.Create(await func(result.Value));
    }
}