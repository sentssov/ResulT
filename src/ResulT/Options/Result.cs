namespace ResulT.Options;

public partial class Result
{
    protected internal Result(bool isSuccess, Error error)
    {
        switch (isSuccess)
        {
            case true when error != Error.None:
                throw new InvalidOperationException();
            case false when error == Error.None:
                throw new InvalidOperationException();
            default:
                IsSuccess = isSuccess;
                Errors = new[] { error };
                break;
        }
    }

    protected internal Result(bool isSuccess, params Error[] errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error[] Errors { get; }
    
    #region Success

    public static Result Success() =>
        new(true, Error.None);

    public static Result<TValue> Success<TValue>(TValue? value) =>
        value is not null 
            ? new Result<TValue>(value, true, Error.None)
            : Failure<TValue>(Error.NullValue);

    #endregion

    #region Failure

    public static Result Failure(Error error) => 
        new(false, error);

    public static Result Failure(params Error[] errors) =>
        new(false, errors);
    
    public static Result<TValue> Failure<TValue>(Error error) =>
        new(default, false, error);

    public static Result<TValue> Failure<TValue>(params Error[] errors) =>
        new(default, false, errors);

    #endregion

    public static Result<TValue> Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;
    
    protected internal Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error) => _value = value;
    protected internal Result(TValue? value, bool isSuccess, params Error[] errors)
        : base(isSuccess, errors) => _value = value;

    public TValue? Value => _value is not null
        ? _value
        : throw new InvalidOperationException();

    public static implicit operator Result<TValue>(TValue? value) => Create(value);
}

public partial class Result
{
    #region Ensure

    public static Result Ensure(bool condition, 
        Error error)
    {
        if (!condition)
            return Failure(error);
        return Success();
    }
    
    public static Result<TValue> Ensure<TValue>(TValue value,
        Func<TValue, bool> predicate, Error error)
    {
        if (!predicate(value))
            return Failure<TValue>(error);
        return Success(value);
    }

    public static Result Ensure(
        (bool condition, Error error)[] conditions)
    {
        return Combine(conditions
            .Select(c => Ensure(c.condition, c.error))
            .ToArray());
    }

    public static Result<TValue> Ensure<TValue>(TValue value,
        (Func<TValue, bool> predicate, Error error)[] functions)
    {
        return Combine(functions
            .Select(p => Ensure(value, p.predicate, p.error))
            .ToArray());
    }

    #endregion
    
    #region Combine

    public static Result<TValue> Combine<TValue>(params Result<TValue>[] results)
    {
        if (results.Any(r => r.IsFailure))
            return Failure<TValue>(results
                .SelectMany(r => r.Errors)
                .Distinct()
                .ToArray());

        return Success(results[0].Value);
    }

    public static Result Combine(params Result[] results)
    {
        if (results.Any(r => r.IsFailure))
            return Failure(results
                .SelectMany(r => r.Errors)
                .Distinct()
                .ToArray());

        return Success();
    }

    #endregion
    
    #region Try

    public static Result<TOut> Try<TOut>(Func<Result<TOut>> func,
        Func<Exception, Error> handler)
    {
        try
        {
            return func();
        }
        catch (Exception exp)
        {
            return Failure<TOut>(handler(exp));
        }
    }

    public static async Task<Result<TOut>> Try<TOut>(Func<Task<Result<TOut>>> func,
        Func<Exception, Error> handler)
    {
        try
        {
            return await func();
        }
        catch (Exception exp)
        {
            return Failure<TOut>(handler(exp));
        }
    }
    
    public static Result Try(Action action, 
        Func<Exception, Error> expHandler)
    {
        try
        {
            action();
            return Success();
        }
        catch (Exception exp)
        {
            return Failure(expHandler(exp));
        }
    }

    public static async Task<Result> Try(Func<Task> func, 
        Func<Exception, Error> expHandler)
    {
        try
        {
            await func();
            return Success();
        }
        catch (Exception exp)
        {
            return Failure(expHandler(exp));
        }
    }
    
    public static Result<TOut> Try<TOut>(Func<TOut?> func, 
        Func<Exception, Error> expHandler)
    {
        try
        {
            return Create(func());
        }
        catch (Exception exp)
        {
            return Failure<TOut>(expHandler(exp));
        }
    }

    public static async Task<Result<TOut>> Try<TOut>(Func<Task<TOut?>> func, 
        Func<Exception, Error> expHandler)
    {
        try
        {
            return Create(await func());
        }
        catch (Exception exp)
        {
            return Failure<TOut>(expHandler(exp));
        }
    }

    #endregion
}