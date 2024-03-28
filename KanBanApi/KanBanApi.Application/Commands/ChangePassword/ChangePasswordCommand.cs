using FastEndpoints;
using KanBanApi.Application.Dtos;

namespace KanBanApi.Application.Commands.ChangePassword;

public record ChangePasswordCommand(string OldPassword, string NewPassword) : ICommand<UserResponse>;