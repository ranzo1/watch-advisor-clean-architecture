using SharedKernel;

namespace Domain.Watches.ValueObjects;

public sealed record DialColor
{
    public string Value { get; }

    private DialColor() { }

    private DialColor(string value) => Value = value;

    public static Result<DialColor> Create(string value)
    {
        Ensure.NotNullOrEmpty(value);

        return new DialColor(value);
    }
}
