using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<Result> Tap(this Result result,
        Func<Task> func)
    {
        if (result.IsFailure)
            return result;

        await func();
        return result;
    }
    
    public static async Task<Result<TIn>> Tap<TIn>(this Result<TIn> result,
        Func<TIn?, Task> func)
    {
        if (result.IsFailure)
            return result;
        
        await func(result.Value);
        return result;
    }
}