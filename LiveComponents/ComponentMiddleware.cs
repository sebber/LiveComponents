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

                _component.CallMethod(action);
            }


            var result = $@"
            <html>
                <head>
                    <title>{type.ToString()}</title>
                    <script src='/js/morphdom.js'></script>
                    <script src='/js/signalr.min.js'></script>
                    <script src='/js/livecomponents.js'></script>
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
