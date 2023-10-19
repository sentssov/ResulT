using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static TOut Complete<TOut>(this Result result,
        Func<Result, TOut> func)
    {
        return func(result);
    }
    
    public static TOut Complete<TIn, TOut>(this Result<TIn> result,
        Func<Result<TIn>, TOut> func)
    {
        return func(result);
    }
}