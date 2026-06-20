using Domain.Watches.Errors;
using SharedKernel;

namespace Domain.Watches.ValueObjects;

public sealed record PriceRange
{
    private PriceRange(decimal min, decimal max, string currency)
    {
        Min = min;
        Max = max;
        Currency = currency;
    }

    public decimal Min { get; }
    public decimal Max { get; }
    public string Currency { get; }

    public static Result<PriceRange> Create(decimal min, decimal max, string? currency)
    {
        if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3 || !currency.All(char.IsLetter))
        {
            return Result.Failure<PriceRange>(PriceRangeErrors.InvalidCurrency);
        }

        if (min < 0)
        {
            return Result.Failure<PriceRange>(PriceRangeErrors.NegativeMinimum);
        }

        if (max <= 0)
        {
            return Result.Failure<PriceRange>(PriceRangeErrors.InvalidMaximum);
        }

        if (min > max)
        {
            return Result.Failure<PriceRange>(PriceRangeErrors.MinimumExceedsMaximum);
        }

        return new PriceRange(min, max, currency.ToUpperInvariant());
    }

    public bool Contains(Price price) => price.Eur >= Min && price.Eur <= Max;
}
