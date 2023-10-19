using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static TOut Complete<TOut>(this Result result,
        Func<Result, TOut> func)
    {
        return func(result);
    }

    public static async Task<TOut> Complete<TOut>(this Task<Result> resultTask,
        Func<Result, TOut> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return func(result);
    }

    public static async Task<TOut> Complete<TOut>(this Result result,
        Func<Result, Task<TOut>> func)
    {
        return await func(result);
    }

    public static async Task<TOut> Complete<TOut>(this Task<Result> resultTask,
        Func<Result, Task<TOut>> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await func(result);
    }
    
    public static TOut Complete<TIn, TOut>(this Result<TIn> result,
        Func<Result<TIn>, TOut> func)
    {
        return func(result);
    }

    public static async Task<TOut> Complete<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<Result<TIn>, TOut> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return func(result);
    }

    public static async Task<TOut> Complete<TIn, TOut>(this Result<TIn> result,
        Func<Result<TIn>, Task<TOut>> func)
    {
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