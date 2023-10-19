using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<Result<TIn>> Ensure<TIn>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<bool>> predicate, Error error)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        
        return await result.Ensure(predicate, error)
            .ConfigureAwait(false);
    }
}