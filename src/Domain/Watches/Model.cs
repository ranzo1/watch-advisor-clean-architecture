using SharedKernel;

namespace Domain.Watches;

public sealed record Model
{
    private Model(string value) => Value = value;

    public string Value { get; }

    public static Result<Model> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<Model>(WatchErrors.EmptyModel);
        }

        return new Model(value.Trim());
    }
}
