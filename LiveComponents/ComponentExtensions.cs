using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LiveComponents
{
    public static class ComponentExtensions
    {
        public static IApplicationBuilder UseLiveComponent<T>(this IApplicationBuilder builder, string path)
            where T : IComponent
        {
            var component = (IComponent)Activator.CreateInstance(typeof(T));

            var registry = builder.ApplicationServices.GetService<ComponentRegistry>();

            registry.RegisterComponent(path, component);

            builder.MapWhen(
                context => context.Request.Path.ToString().EndsWith(path),
                app => app.UseMiddleware<ComponentMiddleware>(component)
            );

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

        public static void CallMethod(this IComponent component, string methodName)
        {
            var method = component.GetType().GetMethod(methodName);

            if (method != null)
            {
                method.Invoke(component, null);
            }
        }
    }
}
