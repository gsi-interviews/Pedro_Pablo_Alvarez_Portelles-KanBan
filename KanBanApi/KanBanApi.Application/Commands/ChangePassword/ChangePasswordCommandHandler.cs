using FastEndpoints;
using KanBanApi.Application.Dtos;
using KanBanApi.Application.Services;
using Microsoft.AspNetCore.Identity;

namespace KanBanApi.Application.Commands.ChangePassword;

public class ChangePasswordCommandHandler(UserManager<IdentityUser> _userManager, IActiveSession _activeSession, IJwtGenerator _jwtGenerator) : CommandHandler<ChangePasswordCommand, UserResponse>
{
    public override async Task<UserResponse> ExecuteAsync(ChangePasswordCommand command, CancellationToken ct = default)
    {
        var username = _activeSession.UserId();

        if (username is null)
            ThrowError("No valid active user");

        var user = await _userManager.FindByIdAsync(username);

        if (user is null) ThrowError($"User '{username}' does not exists");

        if (!await _userManager.CheckPasswordAsync(user, command.OldPassword))
            ThrowError("Wrong old password");

        var result = await _userManager.ChangePasswordAsync(user, command.OldPassword, command.NewPassword);

        if (!result.Succeeded) ThrowError("Error while changing password");

        return new UserResponse(user.Id, user.UserName!, _jwtGenerator.GetToken(user));
    }
}