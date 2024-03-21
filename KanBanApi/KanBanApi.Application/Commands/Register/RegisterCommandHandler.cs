using FastEndpoints;
using KanBanApi.Application.Dtos;
using KanBanApi.Application.Services;
using Microsoft.AspNetCore.Identity;

namespace KanBanApi.Application.Commands.Register;

public class RegisterCommandHandler(UserManager<IdentityUser> _userManager, IJwtGenerator _jwtGenerator) : CommandHandler<RegisterCommand, UserReponse>
{
    public override async Task<UserReponse> ExecuteAsync(RegisterCommand command, CancellationToken ct = default)
    {
        var user = await _userManager.FindByNameAsync(command.Username) ??
                   await _userManager.FindByEmailAsync(command.Username);

        if (user != null) ThrowError("User already exists");

        user = new IdentityUser { UserName = command.Username, Email = command.Email };
        var result = await _userManager.CreateAsync(user, command.Password);

        if (!result.Succeeded) ThrowError("Error while creating user, try again later");

        return new UserReponse(user.Id, user.UserName, _jwtGenerator.GetToken(user));
    }
}