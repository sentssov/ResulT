namespace ResulT.Options;

public class Maybe<TValue>
{
    private Maybe(TValue? value) =>
        Value = value;
    
    public TValue? Value { get; }
    
    public bool HasValue => Value != null;

    public static Maybe<TValue?> Create(TValue? value) => new(value);
}