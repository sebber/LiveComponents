using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace LiveComponents
{
    public class ComponentHub : Hub
    {
        private ComponentRegistry Components { get; }

        public ComponentHub(ComponentRegistry components)
        {
            Components = components;
        }

        public Task CallAction(string path, string action)
        {
            var component = Components.FindComponent(path);

            var type = component.GetType();

            component.CallMethod(action);

            var result = $@"
            <html>
                <head>
                    <title>{type.ToString()}</title>
                    <script src='/js/morphdom.js'></script>
                    <script src='/js/signalr.min.js'></script>
                    <script src='/js/livecomponents.js'></script>
                </head>
                <body>
                    {component.Render()}
                </body>
            </html>
            ";

            return Clients.All.SendAsync("RenderComponent", result);
        }
    }
}
