using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static TIn? Default<TIn>(this Result<TIn> result,
        TIn? defaultValue = default)
    {
        return result.IsFailure 
            ? defaultValue 
            : result.Value;
    }
}