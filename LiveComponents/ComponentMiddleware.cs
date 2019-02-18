using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LiveComponents
{
    public class ComponentMiddleware
    {
        IComponent _component;

        string _id;

        public ComponentMiddleware(RequestDelegate next, string id, IComponent component)
        {
            _component = component;
            _id = id;
        }

        public async Task Invoke(HttpContext context)
        {
            var type = _component.GetType();

            if (context.Request.Headers.ContainsKey("LIVE-COMPONENT-ACTION"))
            {
                var action = context.Request.Headers["LIVE-COMPONENT-ACTION"];

                _component.CallMethod(action, null);
            }


            var result = $@"
            <html>
                <head>
                    <meta charset=""utf-8"">
                    <title>{type.ToString()}</title>
                    <script src=""/js/morphdom.js""></script>
                    <script src=""/js/signalr.min.js""></script>
                    <script src=""/js/livecomponents.js""></script>
                </head>
                <body>
                    <div live-component=""{_id}"">
                       {_component.Render()}
                    </div>
                </body>
            </html>
            ";


            await context.Response.WriteAsync(result);
        }
    }
}
