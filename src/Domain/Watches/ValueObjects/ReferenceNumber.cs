using SharedKernel;

namespace Domain.Watches.ValueObjects;

public sealed record ReferenceNumber
{
    public string Value { get; }

    private ReferenceNumber() { }

    private ReferenceNumber(string value) => Value = value;

    public static Result<ReferenceNumber> Create(string? value)
    {
        Ensure.NotNullOrEmpty(value);

        return new ReferenceNumber(value);
    }
}
