using ResulT.Options;

namespace ResulT.Extensions;

public static partial class ResultExtensions
{
    public static async Task<TIn?> Default<TIn>(this Task<Result<TIn>> resultTask,
        TIn? defaultValue = default)
    {
        var result = await resultTask;
        
        return result.IsFailure 
            ? defaultValue 
            : result.Value;
    }
}