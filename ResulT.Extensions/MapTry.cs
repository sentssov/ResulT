using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static Result<TOut> MapTry<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, TOut> func, Func<Exception, Error> expHandler)
    {
        if (result.IsFailure)
            return Result.Failure<TOut>(result.Errors);
        return Result.Try(() => func(result.Value), expHandler);
    }
    
    public static async Task<Result<TOut>> MapTry<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, TOut> func, Func<Exception, Error> expHandler)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        return result.MapTry(func, expHandler);
    }
    
    public static async Task<Result<TOut>> MapTry<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, Task<TOut>> func, Func<Exception, Error> expHandler)
    {
        if (result.IsFailure)
            return Result.Failure<TOut>(result.Errors);
        return await Result.Try(async () => await func(result.Value), expHandler);
    }
    
    public static async Task<Result<TOut>> MapTry<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<TOut>> func, Func<Exception, Error> expHandler)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        return await result.MapTry(func, expHandler);
    }
}