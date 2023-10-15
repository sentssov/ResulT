using ResulT.Options;

namespace ResulT.Extensions;

public static class ResultExtensions
{
    #region Ensure

    public static Result<TIn> Ensure<TIn>(this Result<TIn> result,
        Func<TIn?, bool> predicate, Error error)
    {
        if (result.IsFailure)
            return result;

        return predicate(result.Value) ? result : Result.Failure<TIn>(error);
    }

    public static async Task<Result<TIn>> Ensure<TIn>(this Task<Result<TIn>> resultTask,
        Func<TIn?, bool> predicate, Error error)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        
        return result.Ensure(predicate, error);
    }
    public static async Task<Result<TIn>> Ensure<TIn>(this Result<TIn> result,
        Func<TIn?, Task<bool>> predicate, Error error)
    {
        if (result.IsFailure)
            return result;

        return await predicate(result.Value) ? result : Result.Failure<TIn>(error);
    }
        
    public static async Task<Result<TIn>> Ensure<TIn>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<bool>> predicate, Error error)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        
        return await result.Ensure(predicate, error)
            .ConfigureAwait(false);
    }

    #endregion

    #region Tap

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
        return Tap(await resultTask, action);
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
        return await Tap(await resultTask, func);
    }
    
    #endregion

    #region Bind

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
        return Bind(await resultTask, func);
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
        return await Bind(await resultTask, func);
    }

    #endregion
    
    #region Map

    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, TOut> func)
    {
        return result.IsFailure
            ? Result.Failure<TOut>(result.Errors)
            : Result.Success(func(result.Value));
    }
        
    public static async Task<Result<TOut>> Map<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, TOut> func)
    {
        return Map(await resultTask, func);
    }
    public static async Task<Result<TOut>> Map<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, Task<TOut>> func)
    {
        return result.IsFailure
            ? Result.Failure<TOut>(result.Errors)
            : Result.Success(await func(result.Value));
    }

    public static async Task<Result<TOut>> Map<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<TOut>> func)
    {
        return await Map(await resultTask, func);
    }
    
    #endregion

    #region Reduce

    public static TIn? Reduce<TIn>(this Result<TIn> result,
        TIn? defaultValue = default)
    {
        return result.IsFailure 
            ? defaultValue 
            : result.Value;
    }

    public static async Task<TIn?> Reduce<TIn>(this Task<Result<TIn>> resultTask,
        TIn? defaultValue = default)
    {
        var result = await resultTask;
        
        return result.IsFailure 
            ? defaultValue 
            : result.Value;
    }
    
    #endregion
}