using Domain.Watches.ValueObjects;
using SharedKernel;

namespace Domain.Watches.Errors;

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

    public static readonly Error InvalidCaseThickness = Error.Problem(
        "Watches.InvalidCaseThickness",
        "Case thickness must be greater than zero");

    public static readonly Error InvalidLugWidth = Error.Problem(
        "Watches.InvalidLugWidth",
        "Lug width must be greater than zero");

    public static readonly Error InvalidLugToLug = Error.Problem(
        "Watches.InvalidLugToLug",
        "Lug-to-lug distance must be greater than zero");

    public static readonly Error InvalidPrice = Error.Problem(
        "Watches.InvalidPrice",
        "Price must be greater than zero");
}
