using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<TOut> Complete<TOut>(this Result result,
        Func<Result, Task<TOut>> func)
    {
        return await func(result);
    }
    
    public static async Task<TOut> Complete<TIn, TOut>(this Result<TIn> result,
        Func<Result<TIn>, Task<TOut>> func)
    {
        return await func(result);
    }
}