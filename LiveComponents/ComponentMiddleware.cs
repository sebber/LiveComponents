using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LiveComponents
{
    public class ComponentMiddleware
    {
        IComponent _component;

        public ComponentMiddleware(RequestDelegate next, IComponent component)
        {
            _component = component;
        }

        public async Task Invoke(HttpContext context)
        {
            var type = _component.GetType();

            if (context.Request.Headers.ContainsKey("LIVE-COMPONENT-ACTION"))
            {
                var action = context.Request.Headers["LIVE-COMPONENT-ACTION"];
                var method = type.GetMethod(action);

                method.Invoke(_component, null);
            }


            var result = $@"
            <html>
                <head>
                    <title>{type.ToString()}</title>
                    <script src='/lib/morphdom.js'></script>
                    <script src='/lib/signalr.min.js'></script>
                    <script src='/test.js'></script>
                </head>
                <body>
                    {_component.Render()}
                </body>
            </html>
            ";


            await context.Response.WriteAsync(result);
        }
    }
}
