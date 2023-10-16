using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
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
        var result = await resultTask
            .ConfigureAwait(false);
        
        return result.Map(func);
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
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.Map(func)
            .ConfigureAwait(false);
    }

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

    public static Result<TOut> MapIf<TIn, TOut>(this Result<TIn> result,
        bool condition, Func<TIn?, TOut> func, Error error)
    {
        if (result.IsFailure)
            return Result.Failure<TOut>(result.Errors);
        return Result.If(condition, () => func(result.Value), error);
    }

    public static async Task<Result<TOut>> MapIf<TIn, TOut>(this Task<Result<TIn>> resultTask,
        bool condition, Func<TIn?, TOut> func, Error error)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        return Result.If(condition, () => func(result.Value), error);
    }

    public static async Task<Result<TOut>> MapIf<TIn, TOut>(this Result<TIn> result,
        bool condition, Func<TIn?, Task<TOut>> func, Error error)
    {
        return await Result.If(condition, async () => await func(result.Value), error);
    }

    public static async Task<Result<TOut>> MapIf<TIn, TOut>(this Task<Result<TIn>> resultTask,
        bool condition, Func<TIn?, Task<TOut>> func, Error error)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        return await Result.If(condition, async () => await func(result.Value), error);
    }

    public static Result<TOut> MapIf<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, bool> predicate, Func<TIn?, TOut> func, Error error)
    {
        if (result.IsFailure)
            return Result.Failure<TOut>(result.Errors);
        return result.MapIf(predicate(result.Value), func, error);
    }
    
    public static async Task<Result<TOut>> MapIf<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, Task<bool>> predicate, Func<TIn?, TOut> func, Error error)
    {
        if (result.IsFailure)
            return Result.Failure<TOut>(result.Errors);
        return result.MapIf(await predicate(result.Value), func, error);
    }

    public static async Task<Result<TOut>> MapIf<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, bool> predicate, Func<TIn?, TOut> func, Error error)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        return result.MapIf(predicate(result.Value), func, error);
    }
    
    public static async Task<Result<TOut>> MapIf<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<bool>> predicate, Func<TIn?, TOut> func, Error error)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        return result.MapIf(await predicate(result.Value), func, error);
    }

    public static async Task<Result<TOut>> MapIf<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, bool> predicate, Func<TIn?, Task<TOut>> func, Error error)
    {
        return await result.MapIf(predicate(result.Value), func, error);
    }
    
    public static async Task<Result<TOut>> MapIf<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, Task<bool>> predicate, Func<TIn?, Task<TOut>> func, Error error)
    {
        return await result.MapIf(await predicate(result.Value), func, error);
    }

    public static async Task<Result<TOut>> MapIf<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, bool> predicate, Func<TIn?, Task<TOut>> func, Error error)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        return await result.MapIf(predicate(result.Value), func, error);
    }
    
    public static async Task<Result<TOut>> MapIf<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<bool>> predicate, Func<TIn?, Task<TOut>> func, Error error)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        return await result.MapIf(await predicate(result.Value), func, error);
    }
}