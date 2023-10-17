using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
        public static Result<TOut> BindIf<TIn, TOut>(this Result<TIn> result,
        bool condition, Func<TIn?, Result<TOut>> func, Error error)
    {
        return result.BindIf(_ => condition, func, error);
    }

    public static async Task<Result<TOut>> BindIf<TIn, TOut>(this Task<Result<TIn>> resultTask,
        bool condition, Func<TIn?, Result<TOut>> func, Error error)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        return result.BindIf(_ => condition, func, error);
    }

    public static async Task<Result<TOut>> BindIf<TIn, TOut>(this Result<TIn> result,
        bool condition, Func<TIn?, Task<Result<TOut>>> func, Error error)
    {
        return await result.BindIf(_ => condition, func, error);
    }

    public static async Task<Result<TOut>> BindIf<TIn, TOut>(this Task<Result<TIn>> resultTask,
        bool condition, Func<TIn?, Task<Result<TOut>>> func, Error error)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        return await result.BindIf(_ => condition, func, error);
    }
    
    public static Result<TOut> BindIf<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, bool> predicate, Func<TIn?, Result<TOut>> func, Error error)
    {
        return result.IsFailure 
            ? Result.Failure<TOut>(result.Errors) 
            : Result.If(predicate(result.Value), () => func(result.Value), error).Bind(x => x!);
    }

    public static async Task<Result<TOut>> BindIf<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, bool> predicate, Func<TIn?, Result<TOut>> func, Error error)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        return result.BindIf(predicate, func, error);
    }

    public static async Task<Result<TOut>> BindIf<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, bool> predicate, Func<TIn?, Task<Result<TOut>>> func, Error error)
    {
        return result.IsFailure
            ? Result.Failure<TOut>(result.Errors)
            : await Result.If(predicate(result.Value), () => func(result.Value), error).Bind(x => x!);
    }

    public static async Task<Result<TOut>> BindIf<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, bool> predicate, Func<TIn?, Task<Result<TOut>>> func, Error error)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        return await result.BindIf(predicate, func, error);
    }

    public static async Task<Result<TOut>> BindIf<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, Task<bool>> predicate, Func<TIn?, Result<TOut>> func, Error error)
    {
        return result.IsFailure
            ? Result.Failure<TOut>(result.Errors)
            : await Result.If(() => predicate(result.Value), () => func(result.Value), error).Bind(x => x!);
    }
    
    public static async Task<Result<TOut>> BindIf<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<bool>> predicate, Func<TIn?, Result<TOut>> func, Error error)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        return await result.BindIf(predicate, func, error);
    }
    
    public static async Task<Result<TOut>> BindIf<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, Task<bool>> predicate, Func<TIn?, Task<Result<TOut>>> func, Error error)
    {
        return result.IsFailure
            ? Result.Failure<TOut>(result.Errors)
            : await Result.If(() => predicate(result.Value), () => func(result.Value), error).Bind(x => x!);
    }
    
    public static async Task<Result<TOut>> BindIf<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<bool>> predicate, Func<TIn?, Task<Result<TOut>>> func, Error error)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        return await result.BindIf(predicate, func, error);
    }
}