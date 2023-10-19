using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<TOut> Complete<TOut>(this Task<Result> resultTask,
        Func<Result, TOut> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return func(result);
    }
    
    public static async Task<TOut> Complete<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<Result<TIn>, TOut> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return func(result);
    }
}