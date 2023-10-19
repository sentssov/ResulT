using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<Result<TOut>> BindTry<TOut>(this Result result,
        Func<Task<Result<TOut>>> func, Func<Exception, Error> handler)
    {
        if (result.IsFailure)
            return Result.Failure<TOut>(result.Errors);

        return await Result.Try(func, handler);
    }
    
    public static async Task<Result> BindTry<TIn>(this Result<TIn> result,
        Func<TIn?, Task<Result>> func, Func<Exception, Error> handler)
    {
        if (result.IsFailure)
            return Result.Failure(result.Errors);

        return await Result.Try(() => func(result.Value)!, handler);
    }
    
    public static async Task<Result<TOut>> BindTry<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, Task<Result<TOut>>> func, Func<Exception, Error> handler)
    {
        if (result.IsFailure)
            return Result.Failure<TOut>(result.Errors);

        return await Result.Try(() => func(result.Value), handler);
    }
}