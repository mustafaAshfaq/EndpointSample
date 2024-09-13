using EndpointSample.Contract;
namespace EndpointSample.Endpoint;

public class GetEndPoint:IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/get", () => "Hello World!");
    }
}