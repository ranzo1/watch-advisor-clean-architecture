using FluentValidation;

namespace Application.Collections.AddToCollection;

internal sealed class AddToCollectionCommandValidator : AbstractValidator<AddToCollectionCommand>
{
    public AddToCollectionCommandValidator()
    {
        RuleFor(c => c.WatchId)
            .NotEmpty();
    }
}
