using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<Result> Tap(this Task<Result> resultTask,
        Action action)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return result.Tap(action);
    }
    
    public static async Task<Result<TIn>> Tap<TIn>(this Task<Result<TIn>> resultTask,
        Action<TIn?> action)
    {
        var result = await resultTask
            .ConfigureAwait(false);
   
        return result.Tap(action);
    }
}