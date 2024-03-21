using FastEndpoints;
using KanBanApi.Application.Commands.Login;
using KanBanApi.Application.Dtos;

namespace KanBanApi.WebApi.Endpoints;

public class LoginEndpoint : Endpoint<LoginCommand, UserReponse>
{
    public override void Configure()
    {
        Post("/auth/login");
        AllowAnonymous();
    }

    public override async Task<UserReponse> ExecuteAsync(LoginCommand req, CancellationToken ct)
    {
        return await req.ExecuteAsync(ct);
    }
}