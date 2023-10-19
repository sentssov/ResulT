using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<Result<TIn>> Ensure<TIn>(this Result<TIn> result,
        Func<TIn?, Task<bool>> predicate, Error error)
    {
        if (result.IsFailure)
            return result;

        return await predicate(result.Value) ? result : Result.Failure<TIn>(error);
    }
}