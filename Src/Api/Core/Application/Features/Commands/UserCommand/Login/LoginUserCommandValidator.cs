using Common.Models.RequestModels.UserRequestModels.LoginRequestModels;
using FluentValidation;

namespace Api.Application.Features.Commands.UserCommand.Login
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.EmailAddress).NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("{PropertyName} not a valid email address");

            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("{PropertyName} shoult at be {MinLength} characters");
        }
    }
}
