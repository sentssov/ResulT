using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<Result<TIn>> Fold<TSrc, TIn>(this Result<TSrc> result,
        TIn seed, Func<TIn, TIn?, Task<TIn>> func) where TSrc : IEnumerable<TIn>
    {
        if (result.IsFailure)
            return Result.Failure<TIn>(result.Errors);

        TIn output = seed;
        foreach (var element in result.Value!)
        {
            output = await func(output, element);
        }

        return Result.Success(output);
    }
}