using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<Result<TOut>> Map<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<TOut>> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.Map(func);
    }
}