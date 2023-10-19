namespace ResulT.Options;

public class Error : IEquatable<Error>
{
    public static readonly Error None = Create(
        string.Empty, string.Empty);
        
    public static readonly Error NullValue = Create(
        "Error.NullValue", "The specified result value is null.");

    private Error(string code, string message) =>
        (Code, Message) = (code, message);
        
    public string Code { get; }
    public string Message { get; }

    public static implicit operator string(Error error) => error.Code;

    public static bool operator ==(Error? a, Error? b)
    {
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;

        return a.Code == b.Code;
    }

    public static bool operator !=(Error? a, Error? b) => !(a == b);
    
    public static Error Create(string code, string message) => 
        new(code, message);

    public bool Equals(Error? other) => this == other;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Error)obj);
    }

    public override int GetHashCode() => HashCode.Combine(Code, Message);
}