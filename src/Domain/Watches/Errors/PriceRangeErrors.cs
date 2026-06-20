using SharedKernel;

namespace Domain.Watches.Errors;

public static class PriceRangeErrors
{
    public static readonly Error InvalidCurrency = Error.Problem(
        "PriceRange.InvalidCurrency",
        "Currency must be a valid 3-letter ISO 4217 code (e.g. EUR, USD)");

    public static readonly Error NegativeMinimum = Error.Problem(
        "PriceRange.NegativeMinimum",
        "Minimum price must be zero or greater");

    public static readonly Error InvalidMaximum = Error.Problem(
        "PriceRange.InvalidMaximum",
        "Maximum price must be greater than zero");

    public static readonly Error MinimumExceedsMaximum = Error.Problem(
        "PriceRange.MinimumExceedsMaximum",
        "Minimum price must not exceed maximum price");
}
