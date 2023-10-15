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
        value is not null ? new Result<TValue>(value, true, Error.None)
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