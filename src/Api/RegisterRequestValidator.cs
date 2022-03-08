using FluentValidation;

namespace Api;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
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
        RuleFor(x => x.Addresses)
            .NotNull()
            .SetValidator(new AddressesValidator());
    }
}

public class AddressValidator : AbstractValidator<AddressDto>
{
    public AddressValidator()
    {
        RuleFor(x => x.State)
            .NotEmpty()
            .MaximumLength(2);
        RuleFor(x => x.City)
            .NotEmpty()
            .MaximumLength(40);
        RuleFor(x => x.Street)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(x => x.ZipCode)
            .NotEmpty()
            .MaximumLength(5);
    }
}

public class AddressesValidator : AbstractValidator<AddressDto[]>
{
    public AddressesValidator()
    {
        RuleFor(x => x)
            .NotNull().Must(x => x?.Length is >= 1 and <= 3)
            .WithName("Addresses")
            .WithMessage("The number of addresses must be between 1 and 3")
            .ForEach(x =>
            {
                x.NotNull();
                x.SetValidator(new AddressValidator());
            });
    }
}

public class EditPersonalInfoRequestValidator : AbstractValidator<EditPersonalInfoRequest>
{
    public EditPersonalInfoRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);
        RuleFor(x => x.Addresses)
            .NotNull()
            .SetValidator(new AddressesValidator());
    }
}
