using System;
using System.Collections.Generic;

namespace LiveComponents
{
    public class ComponentRegistry
    {
        private Dictionary<string, IComponent> Components { get; }

        public ComponentRegistry()
        {
            Components = new Dictionary<string, IComponent>();
        }

        public void RegisterComponent(string path, IComponent component)
        {
            Components.Add(path, component);
        }

        public IComponent FindComponent(string path)
        {
            if (Components.ContainsKey(path))
                return Components[path];

            return null;
        }
    }
}
