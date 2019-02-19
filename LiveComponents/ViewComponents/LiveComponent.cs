
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LiveComponents.ViewComponents
{
    [ViewComponent(Name = "LiveComponent")]
    public class LiveComponent : ViewComponent
    {
        public ComponentRegistry Components { get; private set; }

        public LiveComponent(ComponentRegistry components) => Components = components;

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            return await Task.Run(() => View(Components.FindComponent(id)));
        }
    }

}
