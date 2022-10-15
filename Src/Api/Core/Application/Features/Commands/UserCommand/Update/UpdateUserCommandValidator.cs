using Common.Models.RequestModels.UserRequestModels.UpdateRequestModels;
using FluentValidation;

namespace Application.Features.Commands.UserCommand.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.EmailAddress).NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("{PropertyName} not a valid email address");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("{PropertyName} cannot be blank");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("{PropertyName} cannot be blank");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("{PropertyName} cannot be blank");
        }
    }
}
