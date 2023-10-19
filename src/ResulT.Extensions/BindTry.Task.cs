using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<Result<TOut>> BindTry<TOut>(this Task<Result> resultTask,
        Func<Task<Result<TOut>>> func, Func<Exception, Error> handler)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.BindTry(func, handler);
    }
    
    public static async Task<Result> BindTry<TIn>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<Result>> func, Func<Exception, Error> handler)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.BindTry(func, handler);
    }
    
    public static async Task<Result<TOut>> BindTry<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<Result<TOut>>> func, Func<Exception, Error> expHandler)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        
        return await result.BindTry(func, expHandler);
    }
}