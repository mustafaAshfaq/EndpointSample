using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using EndpointSample.Contract;
namespace EndpointSample.Extension;
public static class EndpointRouteBuilderExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services,Assembly assembly)
    {
        var serviceDescriptors=assembly.DefinedTypes.Where(x => x is { IsAbstract: false, IsInterface: false } 
                                    && x.IsAssignableTo(typeof(IEndpoint)))
                                    .Select(x => ServiceDescriptor.Transient(typeof(IEndpoint), x));
        services.TryAddEnumerable(serviceDescriptors);
        return services;
    }

    public static IApplicationBuilder MapEndpoints(this WebApplication app)
    {
        IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>(); 
        /*assembly.DefinedTypes.Where(x => x is { IsAbstract: false, IsInterface: false }
                                    && x.IsAssignableTo(typeof(IEndpoint)))
                                    .Select(Activator.CreateInstance)
                                    .Cast<IEndpoint>(); */
        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(app);
        }
        return app;
    }
}