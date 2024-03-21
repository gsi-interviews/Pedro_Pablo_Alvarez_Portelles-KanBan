using FastEndpoints;
using KanBanApi.Application.Dtos;

namespace KanBanApi.Application.Commands.Login;

public record LoginCommand(string Username, string Password) : ICommand<UserReponse>;