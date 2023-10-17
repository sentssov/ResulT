using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static Result<TOut> BindTry<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, Result<TOut>> func, Func<Exception, Error> expHandler)
    {
        return result.IsFailure 
            ? Result.Failure<TOut>(result.Errors) 
            : Result.Try(() => func(result.Value), expHandler).Bind(x => x!);
    }

    public static async Task<Result<TOut>> BindTry<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Result<TOut>> func, Func<Exception, Error> expHandler)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        return result.BindTry(func, expHandler);
    }

    public static async Task<Result<TOut>> BindTry<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, Task<Result<TOut>>> func, Func<Exception, Error> expHandler)
    {
        if (result.IsFailure)
            return Result.Failure<TOut>(result.Errors);
        return await Result.Try(() => func(result.Value)!, expHandler).Bind(x => x!);
    }

    public static async Task<Result<TOut>> BindTry<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<Result<TOut>>> func, Func<Exception, Error> expHandler)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        return await result.BindTry(func, expHandler);
    }
}