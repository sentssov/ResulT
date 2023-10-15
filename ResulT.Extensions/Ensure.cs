using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static Result<TIn> Ensure<TIn>(this Result<TIn> result,
        Func<TIn?, bool> predicate, Error error)
    {
        if (result.IsFailure)
            return result;

        return predicate(result.Value) ? result : Result.Failure<TIn>(error);
    }

    public static async Task<Result<TIn>> Ensure<TIn>(this Task<Result<TIn>> resultTask,
        Func<TIn?, bool> predicate, Error error)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        
        return result.Ensure(predicate, error);
    }
    
    public static async Task<Result<TIn>> Ensure<TIn>(this Result<TIn> result,
        Func<TIn?, Task<bool>> predicate, Error error)
    {
        if (result.IsFailure)
            return result;

        return await predicate(result.Value) ? result : Result.Failure<TIn>(error);
    }
        
    public static async Task<Result<TIn>> Ensure<TIn>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<bool>> predicate, Error error)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        
        return await result.Ensure(predicate, error)
            .ConfigureAwait(false);
    }
}