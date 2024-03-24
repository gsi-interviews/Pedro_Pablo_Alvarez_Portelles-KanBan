using FastEndpoints;
using KanBanApi.Application.Commands.GoogleSignIn;
using KanBanApi.Application.Dtos;

namespace KanBanApi.WebApi.Endpoints;

public class GoogleSigninEndpoint : Endpoint<GoogleSignInCommand, UserResponse>
{
    public override void Configure()
    {
        AllowAnonymous();
        Get("/auth/google-signin/{googleToken}");
    }

    public override Task<UserResponse> ExecuteAsync(GoogleSignInCommand req, CancellationToken ct)
    {
        return req.ExecuteAsync(ct);
    }
}
