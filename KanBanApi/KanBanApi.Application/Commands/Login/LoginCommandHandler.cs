using FastEndpoints;
using KanBanApi.Application.Dtos;
using KanBanApi.Application.Services;
using Microsoft.AspNetCore.Identity;

namespace KanBanApi.Application.Commands.Login;

public class LoginCommandHandler(UserManager<IdentityUser> _userManager, IJwtGenerator _jwtGenerator) : CommandHandler<LoginCommand, UserReponse>
{
    public override async Task<UserReponse> ExecuteAsync(LoginCommand command, CancellationToken ct = default)
    {
        var user = await _userManager.FindByNameAsync(command.Username);

        if (user is null) ThrowError($"User '{command.Username}' does not exists");

        if (!await _userManager.CheckPasswordAsync(user, command.Password))
            ThrowError("Wrong Password");

        return new UserReponse(user.Id, user.UserName!, _jwtGenerator.GetToken(user));
    }
}