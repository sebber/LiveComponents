using System;
namespace LiveComponents
{
    public interface IComponent
    {
        void CallMethod(string methodName);

        string Render();
    }
}
