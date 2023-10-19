using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static Result<TOut> MapTry<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, TOut> func, Func<Exception, Error> handler)
    {
        return Result.Try(() => result.Map(func), handler);
    }
}