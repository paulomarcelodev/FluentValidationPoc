using FluentValidation;

namespace Api;

public class RegisterRequestValidator : AbstractValidator<DataContracts.RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(150)
            .EmailAddress();
        RuleFor(x => x.Address)
            .NotEmpty()
            .MaximumLength(150);
    }
}
