using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
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