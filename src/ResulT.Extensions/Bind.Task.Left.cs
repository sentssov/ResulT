using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<Result<TOut>> Bind<TOut>(this Task<Result> resultTask,
        Func<Result<TOut>> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return result.Bind(func);
    }
    
    public static async Task<Result> Bind<TIn>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Result> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return result.Bind(func);
    }
    
    public static async Task<Result<TOut>> Bind<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Result<TOut>> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        
        return result.Bind(func);
    }
}