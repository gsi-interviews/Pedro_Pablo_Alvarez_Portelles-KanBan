using FastEndpoints;
using FluentValidation;
using Google.Apis.Auth;
using KanBanApi.Application.Dtos;

namespace KanBanApi.Application.Commands.GoogleSignIn;

public sealed class GoogleSignInCommandValidator : Validator<GoogleSignInCommand>
{
    public GoogleSignInCommandValidator()
    {
        RuleFor(x => x.GoogleToken)
            .NotEmpty().WithMessage("The token must not be empty")
            .NotNull().WithMessage("The token must not be null")
            .MustAsync(async (idToken, ct) =>
            {
                var scope = CreateScope();

                var config = scope.Resolve<GoogleConfigDto>();

                var validationSettings = new GoogleJsonWebSignature.ValidationSettings { Audience = new[] { config.ClientId } };

                try { var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, validationSettings); return true; }
                catch (InvalidJwtException) { return false; }
            }).WithMessage("Oauth token is not valid");
    }
}