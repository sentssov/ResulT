using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<Result> TapTry(this Result result,
        Func<Task> func, Func<Exception, Error> handler)
    {
        return await Result.Try(() => result.Tap(func)!, handler);
    }
    
    public static async Task<Result<TIn>> TapTry<TIn>(this Result<TIn> result,
        Func<TIn?, Task> func, Func<Exception, Error> handler)
    {
        return await Result.Try(() => result.Tap(func), handler);
    }
}