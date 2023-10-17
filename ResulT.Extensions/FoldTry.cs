using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
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
}