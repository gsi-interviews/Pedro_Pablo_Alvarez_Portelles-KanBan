using FastEndpoints;
using KanBanApi.Application.Commands.Login;
using KanBanApi.Application.Dtos;

namespace KanBanApi.WebApi.Endpoints;

public class LoginEndpoint : Endpoint<LoginCommand, UserResponse>
{
    public override void Configure()
    {
        Post("/auth/login");
        AllowAnonymous();
    }

    public override async Task<UserResponse> ExecuteAsync(LoginCommand req, CancellationToken ct)
    {
        return await req.ExecuteAsync(ct);
    }
}