using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<Result> TapTry(this Task<Result> resultTask,
        Func<Task> func, Func<Exception, Error> handler)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.TapTry(func, handler);
    }
    
    public static async Task<Result<TIn>> TapTry<TIn>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task> func, Func<Exception, Error> handler)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.TapTry(func, handler);
    }
}