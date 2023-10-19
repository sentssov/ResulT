using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static TIn? Reduce<TIn>(this Result<TIn> result,
        TIn? defaultValue = default)
    {
        return result.IsFailure 
            ? defaultValue 
            : result.Value;
    }

    public static async Task<TIn?> Reduce<TIn>(this Task<Result<TIn>> resultTask,
        TIn? defaultValue = default)
    {
        var result = await resultTask;
        
        return result.IsFailure 
            ? defaultValue 
            : result.Value;
    }
}