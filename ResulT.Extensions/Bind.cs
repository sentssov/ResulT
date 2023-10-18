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

    public static async Task<Result<TOut>> Bind<TOut>(this Task<Result> resultTask,
        Func<Result<TOut>> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return result.Bind(func);
    }

    public static async Task<Result<TOut>> Bind<TOut>(this Result result,
        Func<Task<Result<TOut>>> func)
    {
        return result.IsFailure
            ? Result.Failure<TOut>(result.Errors)
            : await func();
    }

    public static async Task<Result<TOut>> Bind<TOut>(this Task<Result> resultTask,
        Func<Task<Result<TOut>>> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.Bind(func);
    }

    public static Result Bind<TIn>(this Result<TIn> result,
        Func<TIn?, Result> func)
    {
        if (result.IsFailure)
            return Result.Failure(result.Errors);

        return func(result.Value);
    }

    public static async Task<Result> Bind<TIn>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Result> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return result.Bind(func);
    }

    public static async Task<Result> Bind<TIn>(this Result<TIn> result,
        Func<TIn?, Task<Result>> func)
    {
        if (result.IsFailure)
            return Result.Failure(result.Errors);

        return await func(result.Value);
    }

    public static async Task<Result> Bind<TIn>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<Result>> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.Bind(func);
    }
    
    public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> result, 
        Func<TIn?, Result<TOut>> func)
    {
        return result.IsFailure 
            ? Result.Failure<TOut>(result.Errors) 
            : func(result.Value);
    }
    
    public static async Task<Result<TOut>> Bind<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Result<TOut>> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        
        return result.Bind(func);
    }
    
    public static async Task<Result<TOut>> Bind<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, Task<Result<TOut>>> func)
    {
        return result.IsFailure 
            ? Result.Failure<TOut>(result.Errors) 
            : await func(result.Value);
    }

    public static async Task<Result<TOut>> Bind<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<Result<TOut>>> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        
        return await result.Bind(func);
    }
}