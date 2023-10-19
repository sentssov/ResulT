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
    
    public static Result BindTry<TIn>(this Result<TIn> result,
        Func<TIn?, Result> func, Func<Exception, Error> handler)
    {
        if (result.IsFailure)
            return Result.Failure(result.Errors);

        return Result.Try(() => func(result.Value!), handler);
    }
    
    public static Result<TOut> BindTry<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, Result<TOut>> func, Func<Exception, Error> handler)
    {
        if (result.IsFailure)
            return Result.Failure<TOut>(result.Errors);

        return Result.Try(() => func(result.Value), handler);
    }
}