using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace LiveComponents
{
    public class ComponentHub : Hub
    {
        private IComponent Component { get; }
        public ComponentHub(ComponentRegistry components)
        {
            Component = components.FindComponent("counter");
        }

        public Task CallAction(string action)
        {
            var type = Component.GetType();

            Component.CallMethod(action);

            var result = $@"
            <html>
                <head>
                    <title>{type.ToString()}</title>
                    <script src='/lib/morphdom.js'></script>
                    <script src='/lib/signalr.min.js'></script>
                    <script src='/test.js'></script>
                </head>
                <body>
                    {Component.Render()}
                </body>
            </html>
            ";

            return Clients.All.SendAsync("RenderComponent", result);
        }
    }
}
