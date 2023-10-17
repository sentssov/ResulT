using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
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