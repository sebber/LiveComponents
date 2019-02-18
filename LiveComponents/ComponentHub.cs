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

        public Task CallAction(string path, Action action)
        {
            var component = Components.FindComponent(path);

            component.CallMethod(action.Name, action.Parameters);

            var result = $@"
            <div live-component=""{path}"">
               {component.Render()}
            </div>
            ";

            return Clients.All.SendAsync("RenderComponent", result);
        }

        public class Action
        {
            public string Name { get; set; }

            public object[] Parameters { get; set; }
        }
    }
}
