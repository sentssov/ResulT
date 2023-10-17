using ResulT.Options;

namespace ResulT.Extensions;

// TODO: Надо реализовать возможность последовательного вызова функций над элементом - Pipe
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
}