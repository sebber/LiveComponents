using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LiveComponents
{
    public class ComponentMiddleware
    {
        IComponent _component;

        string _path;

        public ComponentMiddleware(RequestDelegate next, string path, IComponent component)
        {
            _component = component;
            _path = path;
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
                    <div live-component=""{_path}"">
                       {_component.Render()}
                    </div>
                </body>
            </html>
            ";


            await context.Response.WriteAsync(result);
        }
    }
}
