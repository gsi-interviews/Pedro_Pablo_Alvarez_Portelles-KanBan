using FastEndpoints;
using FluentValidation;

namespace KanBanApi.Application.Commands.ChangePassword;

public class ChangePasswordCommandValidator : Validator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.OldPassword)
            .NotEmpty().NotNull().WithMessage("Old password cannot be empty or null");

        RuleFor(x => x.NewPassword)
            .NotEmpty().NotNull().WithMessage("New password must not be null or empty")
            .MinimumLength(6).WithMessage("New password must have at least 6 characters")
            .Must(IsStrongPassword).WithMessage("New password not strong, change it please");
    }

    private bool IsStrongPassword(string password)
    {
        if (!password.Any(c => char.IsDigit(c)))
            return false;

        if (!password.Any(c => char.IsUpper(c)))
            return false;

        if (!password.Any(c => char.IsLower(c)))
            return false;

        return true;
    }
}
