using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<Result<TIn>> Fold<TSrc, TIn>(this Task<Result<TSrc>> resultTask,
        TIn seed, Func<TIn, TIn?, Task<TIn>> func) where TSrc : IEnumerable<TIn>
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.Fold(seed, func);
    }
}