using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<Result> TapTry(this Task<Result> resultTask,
        Action action, Func<Exception, Error> handler)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return result.TapTry(action, handler);
    }
    
    public static async Task<Result<TIn>> TapTry<TIn>(this Task<Result<TIn>> resultTask,
        Action<TIn?> action, Func<Exception, Error> handler)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return result.TapTry(action, handler);
    }
}