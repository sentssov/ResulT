using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static Result Tap(this Result result, 
        Action action)
    {
        if (result.IsFailure)
            return result;

        action();
        return result;
    }
    
    public static Result<TIn> Tap<TIn>(this Result<TIn> result,
        Action<TIn?> action)
    {
        if (result.IsFailure)
            return result;

        action(result.Value);
        return result;
    }
}