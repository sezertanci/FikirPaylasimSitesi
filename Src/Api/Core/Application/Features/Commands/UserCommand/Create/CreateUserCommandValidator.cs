using Common.Models.RequestModels.UserRequestModels.CreateRequestModels;
using FluentValidation;

namespace Application.Features.Commands.UserCommand.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.EmailAddress).NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("{PropertyName} not a valid email address");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("{PropertyName} cannot be blank");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("{PropertyName} cannot be blank");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("{PropertyName} cannot be blank");
            RuleFor(x => x.PasswordStr).NotEmpty().MinimumLength(6).WithMessage("{PropertyName} shoult at be {MinLength} characters");
        }
    }
}
