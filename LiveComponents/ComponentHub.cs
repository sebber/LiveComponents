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
            <div live-component=""{path}"">
               {component.Render()}
            </div>
            ";

            return Clients.All.SendAsync("RenderComponent", result);
        }
    }
}
