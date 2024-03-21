using FastEndpoints;

namespace KanBanApi.WebApi.Endpoints;

public class A
{
    public string Message { get; set; } = null!;
}

public class RegisterEndpoint : EndpointWithoutRequest<A>
{
    public override void Configure()
    {
        Get("/");
        AllowAnonymous();
    }

    public override Task<A> ExecuteAsync(CancellationToken ct)
    {
        return Task.FromResult(new A { Message = "Hello world" });
    }
}