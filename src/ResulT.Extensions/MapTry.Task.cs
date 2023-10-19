using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<Result<TOut>> MapTry<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<TOut>> func, Func<Exception, Error> handler)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        
        return await result.MapTry(func, handler);
    }
}