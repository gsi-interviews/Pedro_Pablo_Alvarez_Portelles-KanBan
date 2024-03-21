using FastEndpoints;
using KanBanApi.Application.Dtos;

namespace KanBanApi.Application.Commands.Register;

public record RegisterCommand(string Username, string Password, string Email) : ICommand<UserResponse>;