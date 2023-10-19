namespace ResulT.Options;

public class Result
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