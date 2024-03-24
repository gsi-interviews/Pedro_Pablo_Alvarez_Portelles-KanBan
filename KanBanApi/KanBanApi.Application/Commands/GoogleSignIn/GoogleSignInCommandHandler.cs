using FastEndpoints;
using Google.Apis.Auth;
using KanBanApi.Application.Dtos;
using KanBanApi.Application.Services;
using Microsoft.AspNetCore.Identity;

namespace KanBanApi.Application.Commands.GoogleSignIn;

public class GoogleSignInCommandHandler(UserManager<IdentityUser> _userManager, IJwtGenerator _jwtGenerator, GoogleConfigDto _googleConfig) : CommandHandler<GoogleSignInCommand, UserResponse>
{
    public override async Task<UserResponse> ExecuteAsync(GoogleSignInCommand command, CancellationToken ct = default)
    {
        var validationSettings = new GoogleJsonWebSignature.ValidationSettings { Audience = new[] { _googleConfig.ClientId } };
        var payload = await GoogleJsonWebSignature.ValidateAsync(command.GoogleToken, validationSettings);

        var user = await _userManager.FindByEmailAsync(payload.Email);

        if (user == null)
        {
            user = new IdentityUser
            {
                Email = payload.Email,
                UserName = GetUserName(payload.Email)
            };

            await _userManager.CreateAsync(user);
        }


        var response = new UserResponse(user.Id, user.UserName!, _jwtGenerator.GetToken(user));

        return response;
    }

    private static string GetUserName(string Email)
    {
        return Email.Split('@')[0];
    }
}