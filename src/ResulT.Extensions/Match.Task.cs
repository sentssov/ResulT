using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<TOut> Match<TOut>(this Task<Result> resultTask,
        Func<Task<TOut>> onSuccess, Func<Error[], Task<TOut>> onFailure)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.Match(onSuccess, onFailure);
    }
    
    public static async Task<TOut> Match<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<TOut>> onSuccess, Func<Error[], Task<TOut>> onFailure)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.Match(onSuccess, onFailure);
    }
}