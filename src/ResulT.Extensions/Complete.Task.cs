using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<TOut> Complete<TOut>(this Task<Result> resultTask,
        Func<Result, Task<TOut>> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await func(result);
    }
    
    public static async Task<TOut> Complete<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<Result<TIn>, Task<TOut>> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await func(result);
    }
}