using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static TOut Match<TOut>(this Result result,
        Func<TOut> onSuccess, Func<Error[], TOut> onFailure)
    {
        if (result.IsFailure)
            return onFailure(result.Errors);
        return onSuccess();
    }
    
    public static TOut Match<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, TOut> onSuccess, Func<Error[], TOut> onFailure)
    {
        if (result.IsFailure)
            return onFailure(result.Errors);
        return onSuccess(result.Value);
    }
}