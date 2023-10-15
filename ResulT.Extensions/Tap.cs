using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static Result<TIn> Tap<TIn>(this Result<TIn> result,
        Action<TIn?> action)
    {
        if (result.IsSuccess)
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
        if (result.IsSuccess)
            await func(result.Value);
        return result;
    }

    public static async Task<Result<TIn>> Tap<TIn>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        
        return await result.Tap(func)
            .ConfigureAwait(false);
    }
}