using FluentValidation;

namespace Mediaspot.Application.Titles.Commands;

public sealed class CreateTitleValidator : AbstractValidator<CreateTitleCommand>
{
    public CreateTitleValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

