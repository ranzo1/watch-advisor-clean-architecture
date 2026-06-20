using Domain.Watches.Errors;
using SharedKernel;

namespace Domain.Watches.ValueObjects;

public sealed record Price
{
    private Price(decimal eur) => Eur = eur;

    public decimal Eur { get; }

    public static Result<Price> Create(decimal eur)
    {
        if (eur <= 0)
        {
            return Result.Failure<Price>(WatchErrors.InvalidPrice);
        }

        return new Price(eur);
    }

    public bool IsWithinBudget(decimal budgetEur) => Eur <= budgetEur;
}
