using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<Result<TIn>> FoldTry<TSrc, TIn>(this Result<TSrc> result,
        TIn seed, Func<TIn, TIn?, Task<TIn>> func, Func<Exception, Error> handler)
        where TSrc : IEnumerable<TIn>
    {
        if (result.IsFailure)
            return Result.Failure<TIn>(result.Errors);

        return await Result.Try(() => result.Fold(seed, func), handler);
    }
}