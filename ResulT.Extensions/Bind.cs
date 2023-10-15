using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> result, 
        Func<TIn?, Result<TOut>> func)
    {
        return result.IsFailure 
            ? Result.Failure<TOut>(result.Errors) 
            : func(result.Value);
    }
    
    public static async Task<Result<TOut>> Bind<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Result<TOut>> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        
        return result.Bind(func);
    }
    
    public static async Task<Result<TOut>> Bind<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, Task<Result<TOut>>> func)
    {
        return result.IsFailure 
            ? Result.Failure<TOut>(result.Errors) 
            : await func(result.Value);
    }

    public static async Task<Result<TOut>> Bind<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<Result<TOut>>> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        
        return await result.Bind(func);
    }
}