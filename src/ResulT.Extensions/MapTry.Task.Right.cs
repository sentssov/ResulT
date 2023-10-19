using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<Result<TOut>> MapTry<TIn, TOut>(this Result<TIn> result,
        Func<TIn?, Task<TOut>> func, Func<Exception, Error> handler)
    {
        return await Result.Try(() => result.Map(func), handler);
    }
}