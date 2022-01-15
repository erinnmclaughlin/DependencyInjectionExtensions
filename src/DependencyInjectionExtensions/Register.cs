using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DependencyInjectionExtensions
{
    public static class Register
    {
        public static IServiceCollection FindAndRegisterServices(this IServiceCollection services, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
                services.FindAndRegisterServices(assembly);

            return services;
        }

        public static IServiceCollection FindAndRegisterServices(this IServiceCollection services, Assembly assembly)
        {
            var interfaces = assembly.GetTypes()
                .Where(t => t.IsInterface && typeof(IDependencyScope).IsAssignableFrom(t))
                .ToList();

            foreach (var i in interfaces)
            {
                assembly.GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && i.IsAssignableFrom(t))
                    .ToList().ForEach(t => services.RegisterService(i, t));
            }

            return services;
        }

        private static void RegisterService(this IServiceCollection services, Type abstraction, Type implementation)
        {
            if (typeof(ISingleton).IsAssignableFrom(abstraction))
                services.AddSingleton(abstraction, implementation);

            else if (typeof(IScoped).IsAssignableFrom(abstraction))
                services.AddScoped(abstraction, implementation);

            else if (typeof(ITransient).IsAssignableFrom(abstraction))
                services.AddTransient(abstraction, implementation);
        }
    }
}
