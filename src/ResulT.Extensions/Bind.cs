using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static Result<TOut> Bind<TOut>(this Result result,
        Func<Result<TOut>> func)
    {
        return result.IsFailure
            ? Result.Failure<TOut>(result.Errors)
            : func();
    }
    
    public static Result Bind<TIn>(this Result<TIn> result,
        Func<TIn?, Result> func)
    {
        if (result.IsFailure)
            return Result.Failure(result.Errors);

        return func(result.Value);
    }
    
    public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> result, 
        Func<TIn?, Result<TOut>> func)
    {
        return result.IsFailure 
            ? Result.Failure<TOut>(result.Errors) 
            : func(result.Value);
    }
}