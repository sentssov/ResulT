using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<Result> Tap(this Task<Result> resultTask,
        Func<Task> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.Tap(func);
    }
    
    public static async Task<Result<TIn>> Tap<TIn>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.Tap(func);
    }
}