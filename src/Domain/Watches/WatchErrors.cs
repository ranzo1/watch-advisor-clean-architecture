using SharedKernel;

namespace Domain.Watches;

public static class WatchErrors
{
    public static Error NotFound(Guid watchId) => Error.NotFound(
        "Watches.NotFound",
        $"The watch with the Id = '{watchId}' was not found");

    public static readonly Error EmptyBrand = Error.Problem(
        "Watches.EmptyBrand",
        "Brand must not be empty");

    public static readonly Error EmptyModel = Error.Problem(
        "Watches.EmptyModel",
        "Model must not be empty");

    public static readonly Error InvalidCaseDiameter = Error.Problem(
        "Watches.InvalidCaseDiameter",
        $"Case diameter must be between {CaseDiameter.MinMm} mm and {CaseDiameter.MaxMm} mm");

    public static readonly Error InvalidPrice = Error.Problem(
        "Watches.InvalidPrice",
        "Price must be greater than zero");
}
