using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static Result<TOut> BindTry<TOut>(this Result result,
        Func<Result<TOut>> func, Func<Exception, Error> handler)
    {
        if (result.IsFailure)
            return Result.Failure<TOut>(result.Errors);

        return Result.Try(func, handler);
    }

    public static async Task<Result<TOut>> BindTry<TOut>(this Task<Result> resultTask,
        Func<Result<TOut>> func, Func<Exception, Error> handler)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return result.BindTry(func, handler);
    }

    public static async Task<Result<TOut>> BindTry<TOut>(this Result result,
        Func<Task<Result<TOut>>> func, Func<Exception, Error> handler)
    {
        if (result.IsFailure)
            return Result.Failure<TOut>(result.Errors);

        return await Result.Try(func, handler);
    }

    public static async Task<Result<TOut>> BindTry<TOut>(this Task<Result> resultTask,
        Func<Task<Result<TOut>>> func, Func<Exception, Error> handler)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.BindTry(func, handler);
    }

    public static Result BindTry<TIn>(this Result<TIn> result,
        Func<TIn?, Result> func, Func<Exception, Error> handler)
    {
        if (result.IsFailure)
            return Result.Failure(result.Errors);

        return Result.Try(() => func(result.Value!), handler);
    }

    public static async Task<Result> BindTry<TIn>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Result> func, Func<Exception, Error> handler)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return result.BindTry(func, handler);
    }

    public static async Task<Result> BindTry<TIn>(this Result<TIn> result,
        Func<TIn?, Task<Result>> func, Func<Exception, Error> handler)
    {
        if (result.IsFailure)
            return Result.Failure(result.Errors);

        return await Result.Try(() => func(result.Value)!, handler);
    }

    public static async Task<Result> BindTry<TIn>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<Result>> func, Func<Exception, Error> handler)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.BindTry(func, handler);
    }
    
    public static Result<TOut> BindTry<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, Result<TOut>> func, Func<Exception, Error> handler)
    {
        if (result.IsFailure)
            return Result.Failure<TOut>(result.Errors);

        return Result.Try(() => func(result.Value), handler);
    }

    public static async Task<Result<TOut>> BindTry<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Result<TOut>> func, Func<Exception, Error> handler)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return result.BindTry(func, handler);
    }

    public static async Task<Result<TOut>> BindTry<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, Task<Result<TOut>>> func, Func<Exception, Error> handler)
    {
        if (result.IsFailure)
            return Result.Failure<TOut>(result.Errors);

        return await Result.Try(() => func(result.Value), handler);
    }
    
    public static async Task<Result<TOut>> BindTry<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<Result<TOut>>> func, Func<Exception, Error> expHandler)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        
        return await result.BindTry(func, expHandler);
    }
}