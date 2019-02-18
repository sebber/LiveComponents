using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LiveComponents
{
    public static class ComponentExtensions
    {
        public static IApplicationBuilder UseLiveComponent<T>(this IApplicationBuilder builder, string id, bool mountPath = false)
            where T : IComponent
        {
            var component = (IComponent)Activator.CreateInstance(typeof(T));

            var registry = builder.ApplicationServices.GetService<ComponentRegistry>();

            registry.RegisterComponent(id, component);

            if (mountPath)
            {
                builder.MapWhen(
                    context => context.Request.Path.ToString().EndsWith(id, StringComparison.InvariantCulture),
                    app => app.UseMiddleware<ComponentMiddleware>(id, component)
                );
            }

            builder.UseSignalR(routes =>
            {
                routes.MapHub<ComponentHub>($"/componentHub");
            });

            return builder;
        }

        public static IServiceCollection AddLiveComponents(this IServiceCollection services)
        {
            services.AddSingleton<ComponentRegistry>();

            services.ConfigureOptions(typeof(UIConfigureOptions));

            return services;
        }

        public static void CallMethod(this IComponent component, string methodName, object[] parameters)
        {
            var method = component.GetType().GetMethod(methodName);

            if (method != null)
            {
                method.Invoke(component, parameters);
            }
        }
    }
}
