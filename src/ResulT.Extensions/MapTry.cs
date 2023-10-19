using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static Result<TOut> MapTry<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, TOut> func, Func<Exception, Error> handler)
    {
        return Result.Try(() => result.Map(func), handler);
    }
    
    public static async Task<Result<TOut>> MapTry<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, TOut> func, Func<Exception, Error> handler)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        
        return result.MapTry(func, handler);
    }
    
    public static async Task<Result<TOut>> MapTry<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, Task<TOut>> func, Func<Exception, Error> handler)
    {
        
        return await Result.Try(() => result.Map(func), handler);
    }
    
    public static async Task<Result<TOut>> MapTry<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<TOut>> func, Func<Exception, Error> handler)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        
        return await result.MapTry(func, handler);
    }
}