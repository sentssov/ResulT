using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static Result<TIn> Fold<TSrc, TIn>(this Result<TSrc> result,
        TIn seed, Func<TIn, TIn?, TIn> func) where TSrc : IEnumerable<TIn>
    {
        if (result.IsFailure)
            return Result.Failure<TIn>(result.Errors);

        TIn output = seed;
        foreach (var element in result.Value!)
        {
            output = func(output, element);
        }

        return Result.Success(output);
    }
    public static async Task<Result<TIn>> Fold<TSrc, TIn>(this Task<Result<TSrc>> resultTask,
        TIn seed, Func<TIn, TIn?, TIn> func) where TSrc : IEnumerable<TIn>
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return result.Fold(seed, func);
    }
    public static async Task<Result<TIn>> Fold<TSrc, TIn>(this Result<TSrc> result,
        TIn seed, Func<TIn, TIn?, Task<TIn>> func) where TSrc : IEnumerable<TIn>
    {
        if (result.IsFailure)
            return Result.Failure<TIn>(result.Errors);

        TIn output = seed;
        foreach (var element in result.Value!)
        {
            output = await func(output, element);
        }

        return Result.Success(output);
    }
    public static async Task<Result<TIn>> Fold<TSrc, TIn>(this Task<Result<TSrc>> resultTask,
        TIn seed, Func<TIn, TIn?, Task<TIn>> func) where TSrc : IEnumerable<TIn>
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.Fold(seed, func);
    }

    public static Result<TIn> FoldTry<TSrc, TIn>(this Result<TSrc> result,
        TIn seed, Func<TIn, TIn?, TIn> func, Func<Exception, Error> expHandler) 
        where TSrc : IEnumerable<TIn>
    {
        if (result.IsFailure)
            return Result.Failure<TIn>(result.Errors);

        return Result.Try(() =>
        {
            TIn output = seed;
            foreach (var element in result.Value!)
            {
                output = func(output, element);
            }
            return output;
        }, expHandler);
    }
    public static async Task<Result<TIn>> FoldTry<TSrc, TIn>(this Task<Result<TSrc>> resultTask,
        TIn seed, Func<TIn, TIn?, TIn> func, Func<Exception, Error> expHandler)
        where TSrc : IEnumerable<TIn>
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return result.FoldTry(seed, func, expHandler);
    }
    public static async Task<Result<TIn>> FoldTry<TSrc, TIn>(this Result<TSrc> result,
        TIn seed, Func<TIn, TIn?, Task<TIn>> func, Func<Exception, Error> expHandler)
        where TSrc : IEnumerable<TIn>
    {
        if (result.IsFailure)
            return Result.Failure<TIn>(result.Errors);

        return await Result.Try(async () =>
        {
            TIn output = seed;
            foreach (var element in result.Value!)
            {
                output = await func(output, element);
            }
            return output;
        }, expHandler);
    }
    public static async Task<Result<TIn>> FoldTry<TSrc, TIn>(this Task<Result<TSrc>> resultTask,
        TIn seed, Func<TIn, TIn?, Task<TIn>> func, Func<Exception, Error> expHandler) 
        where TSrc : IEnumerable<TIn>
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.FoldTry(seed, func, expHandler);
    }
    
    public static Result<TIn> FoldIf<TSrc, TIn>(this Result<TSrc> result,
        bool condition, TIn seed, Func<TIn, TIn?, TIn> func, Error error) 
        where TSrc : IEnumerable<TIn>
    {
        if (result.IsFailure)
            return Result.Failure<TIn>(result.Errors);

        if (!condition)
            return Result.Failure<TIn>(error);

        return result.Fold(seed, func);
    }
    public static async Task<Result<TIn>> FoldIf<TSrc, TIn>(this Task<Result<TSrc>> resultTask,
        bool condition, TIn seed, Func<TIn, TIn?, TIn> func, Error error)
        where TSrc : IEnumerable<TIn>
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return result.FoldIf(condition, seed, func, error);
    }
    public static async Task<Result<TIn>> FoldIf<TSrc, TIn>(this Result<TSrc> result,
        bool condition, TIn seed, Func<TIn, TIn?, Task<TIn>> func, Error error)
        where TSrc : IEnumerable<TIn>
    {
        if (result.IsFailure)
            return Result.Failure<TIn>(result.Errors);

        if (!condition)
            return Result.Failure<TIn>(error);

        return await result.Fold(seed, func);
    }
    public static async Task<Result<TIn>> FoldIf<TSrc, TIn>(this Task<Result<TSrc>> resultTask,
        bool condition, TIn seed, Func<TIn, TIn?, Task<TIn>> func, Error error)
        where TSrc : IEnumerable<TIn>
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.FoldIf(condition, seed, func, error);
    }
}