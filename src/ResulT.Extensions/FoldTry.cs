using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static Result<TIn> FoldTry<TSrc, TIn>(this Result<TSrc> result,
        TIn seed, Func<TIn, TIn?, TIn> func, Func<Exception, Error> handler) 
        where TSrc : IEnumerable<TIn>
    {
        if (result.IsFailure)
            return Result.Failure<TIn>(result.Errors);

        return Result.Try(() => result.Fold(seed, func), handler);
    }
}