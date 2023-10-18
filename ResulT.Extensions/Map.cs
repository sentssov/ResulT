using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, TOut> func)
    {
        if (result.IsFailure)
            return Result.Failure<TOut>(result.Errors);

        return Result.Create(func(result.Value));
    }

    public static async Task<Result<TOut>> Map<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, TOut> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return result.Map(func);
    }

    public static async Task<Result<TOut>> Map<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, Task<TOut>> func)
    {
        if (result.IsFailure)
            return Result.Failure<TOut>(result.Errors);

        return Result.Create(await func(result.Value));
    }

    public static async Task<Result<TOut>> Map<TIn, TOut>(this Task<Result<TIn>> resultTask,
        Func<TIn?, Task<TOut>> func)
    {
        var result = await resultTask
            .ConfigureAwait(false);

        return await result.Map(func);
    }
}