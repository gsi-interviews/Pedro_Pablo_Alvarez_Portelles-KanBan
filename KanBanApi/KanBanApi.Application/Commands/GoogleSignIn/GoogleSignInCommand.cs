using FastEndpoints;
using KanBanApi.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace KanBanApi.Application.Commands.GoogleSignIn;

public record GoogleSignInCommand : ICommand<UserResponse>
{
    [FromRoute]
    public string GoogleToken { get; set; } = null!;
}