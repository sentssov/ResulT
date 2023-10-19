using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static Result TapTry(this Result result,
        Action action, Func<Exception, Error> handler)
    {
        return Result.Try(() => result.Tap(action), handler);
    }
    
    public static Result<TIn> TapTry<TIn>(this Result<TIn> result,
        Action<TIn?> action, Func<Exception, Error> handler)
    {
        return Result.Try(() => result.Tap(action), handler);
    }
}