using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<Result<TIn>> Ensure<TIn>(this Task<Result<TIn>> resultTask,
        bool condition, Error error)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        
        return result.Ensure(condition, error);
    }
    
    public static async Task<Result<TIn>> Ensure<TIn>(this Task<Result<TIn>> resultTask,
        Func<TIn?, bool> predicate, Error error)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        
        return result.Ensure(predicate, error);
    }
}