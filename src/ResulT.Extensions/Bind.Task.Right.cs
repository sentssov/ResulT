using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<Result<TOut>> Bind<TOut>(this Result result,
        Func<Task<Result<TOut>>> func)
    {
        return result.IsFailure
            ? Result.Failure<TOut>(result.Errors)
            : await func();
    }
    
    public static async Task<Result> Bind<TIn>(this Result<TIn> result,
        Func<TIn?, Task<Result>> func)
    {
        if (result.IsFailure)
            return Result.Failure(result.Errors);

        return await func(result.Value);
    }
    
    public static async Task<Result<TOut>> Bind<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, Task<Result<TOut>>> func)
    {
        return result.IsFailure 
            ? Result.Failure<TOut>(result.Errors) 
            : await func(result.Value);
    }
}