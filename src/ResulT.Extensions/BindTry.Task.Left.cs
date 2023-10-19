using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<Result<TOut>> BindTry<TOut>(this Task<Result> resultTask,
        Func<Result<TOut>> func, Func<Exception, Error> handler)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return result.BindTry(func, handler);
    }
    
    public static async Task<Result> BindTry<TIn>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Result> func, Func<Exception, Error> handler)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return result.BindTry(func, handler);
    }
    
    public static async Task<Result<TOut>> BindTry<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Result<TOut>> func, Func<Exception, Error> handler)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return result.BindTry(func, handler);
    }
}