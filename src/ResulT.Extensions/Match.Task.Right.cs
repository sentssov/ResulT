using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<TOut> Match<TOut>(this Result result,
        Func<Task<TOut>> onSuccess, Func<Error[], Task<TOut>> onFailure)
    {
        if (result.IsFailure)
            return await onFailure(result.Errors);
        return await onSuccess();
    }
    
    public static async Task<TOut> Match<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, Task<TOut>> onSuccess, Func<Error[], Task<TOut>> onFailure)
    {
        if (result.IsFailure)
            return await onFailure(result.Errors);
        return await onSuccess(result.Value);
    }
}