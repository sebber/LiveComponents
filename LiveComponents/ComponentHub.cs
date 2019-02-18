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

        public Task CallAction(string id, Action action)
        {
            var component = Components.FindComponent(id);

            component.CallMethod(action.Name, action.Parameters);

            return Clients.All.SendAsync("RenderComponent", id, component.Render());
        }

        public class Action
        {
            public string Name { get; set; }

            public object[] Parameters { get; set; }
        }
    }
}
