using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static Result Tap(this Result result, 
        Action action)
    {
        if (result.IsFailure)
            return result;

        action();
        return result;
    }

    public static async Task<Result> Tap(this Task<Result> resultTask,
        Action action)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return result.Tap(action);
    }

    public static async Task<Result> Tap(this Result result,
        Func<Task> func)
    {
        if (result.IsFailure)
            return result;

        await func();
        return result;
    }

    public static async Task<Result> Tap(this Task<Result> resultTask,
        Func<Task> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.Tap(func);
    }
    
    public static Result<TIn> Tap<TIn>(this Result<TIn> result,
        Action<TIn?> action)
    {
        if (result.IsFailure)
            return result;

        action(result.Value);
        return result;
    }
    
    public static async Task<Result<TIn>> Tap<TIn>(this Task<Result<TIn>> resultTask,
        Action<TIn?> action)
    {
        var result = await resultTask
            .ConfigureAwait(false);
   
        return result.Tap(action);
    }
    
    public static async Task<Result<TIn>> Tap<TIn>(this Result<TIn> result,
        Func<TIn?, Task> func)
    {
        if (result.IsFailure)
            return result;
        
        await func(result.Value);
        return result;
    }
    
    public static async Task<Result<TIn>> Tap<TIn>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.Tap(func);
    }
}