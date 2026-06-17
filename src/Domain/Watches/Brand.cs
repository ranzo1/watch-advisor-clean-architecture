using SharedKernel;

namespace Domain.Watches;

public sealed record Brand
{
    private Brand(string value) => Value = value;

    public string Value { get; }

    public static Result<Brand> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<Brand>(WatchErrors.EmptyBrand);
        }

        return new Brand(value.Trim());
    }
}
