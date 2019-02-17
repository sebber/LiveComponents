using System;
namespace LiveComponents
{
    public abstract class BaseComponent : IComponent
    {
        public void CallMethod(string methodName)
        {
            var method = this.GetType().GetMethod(methodName);

            if (method != null)
            {
                method.Invoke(this, null);
            }
        }

        public abstract string Render();
    }
}
