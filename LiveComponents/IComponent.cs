using System;
using Microsoft.AspNetCore.Mvc;

namespace LiveComponents
{
    public interface IComponent
    {
        string Render();
    }
}
