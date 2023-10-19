using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static Result TapTry(this Result result,
        Action action, Func<Exception, Error> handler)
    {
        return Result.Try(() => result.Tap(action), handler);
    }

    public static async Task<Result> TapTry(this Task<Result> resultTask,
        Action action, Func<Exception, Error> handler)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return result.TapTry(action, handler);
    }

    public static async Task<Result> TapTry(this Result result,
        Func<Task> func, Func<Exception, Error> handler)
    {
        return await Result.Try(() => result.Tap(func)!, handler);
    }

    public static async Task<Result> TapTry(this Task<Result> resultTask,
        Func<Task> func, Func<Exception, Error> handler)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.TapTry(func, handler);
    }
    
    public static Result<TIn> TapTry<TIn>(this Result<TIn> result,
        Action<TIn?> action, Func<Exception, Error> handler)
    {
        return Result.Try(() => result.Tap(action), handler);
    }
    
    public static async Task<Result<TIn>> TapTry<TIn>(this Task<Result<TIn>> resultTask,
        Action<TIn?> action, Func<Exception, Error> handler)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return result.TapTry(action, handler);
    }
    
    public static async Task<Result<TIn>> TapTry<TIn>(this Result<TIn> result,
        Func<TIn?, Task> func, Func<Exception, Error> handler)
    {
        return await Result.Try(() => result.Tap(func), handler);
    }
    
    public static async Task<Result<TIn>> TapTry<TIn>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task> func, Func<Exception, Error> handler)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.TapTry(func, handler);
    }
}