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

        RuleFor(x => x.Address).NotNull().SetValidator(new AddressValidator()!);
    }
}

public class AddressValidator : AbstractValidator<DataContracts.AddressDto>
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


public class EditPersonalInfoRequestValidator : AbstractValidator<DataContracts.EditPersonalInfoRequest>
{
    public EditPersonalInfoRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);
        RuleFor(x => x.Address).NotNull().SetValidator(new AddressValidator()!);
    }
}
