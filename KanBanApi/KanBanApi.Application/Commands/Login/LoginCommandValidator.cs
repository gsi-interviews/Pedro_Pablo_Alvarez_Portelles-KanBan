using FastEndpoints;
using FluentValidation;

namespace KanBanApi.Application.Commands.Login;

public class LoginCommandValidator : Validator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotNull().NotEmpty().WithMessage("Username cannot be null or empty");

        RuleFor(x => x.Password)
            .NotEmpty().NotNull().WithMessage("Password must not be null or empty");
    }
}
