namespace ResulT.Options;

public class Maybe<TValue>
{
    private Maybe(TValue? value) =>
        Value = value;
    
    public TValue? Value { get; }
    public bool HasValue => Value is not null;
    
    public static implicit operator Maybe<TValue?>(TValue? value) => 
        Create(value);
    public static Maybe<TValue?> Create(TValue? value) => 
        new(value);
}