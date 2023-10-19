using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<Result<TOut>> Bind<TOut>(this Task<Result> resultTask,
        Func<Task<Result<TOut>>> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.Bind(func);
    }
    
    public static async Task<Result> Bind<TIn>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<Result>> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.Bind(func);
    }
    
    public static async Task<Result<TOut>> Bind<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<Result<TOut>>> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);
        
        return await result.Bind(func);
    }
}