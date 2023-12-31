using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static Result<TIn> Fold<TSrc, TIn>(this Result<TSrc> result,
        TIn seed, Func<TIn, TIn?, TIn> func) where TSrc : IEnumerable<TIn>
    {
        if (result.IsFailure)
            return Result.Failure<TIn>(result.Errors);

        TIn output = seed;
        foreach (var element in result.Value!)
        {
            output = func(output, element);
        }

        return Result.Success(output);
    }
}