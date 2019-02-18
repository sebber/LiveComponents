using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LiveComponents.Samples.ChatRoom.Pages
{
    public class IndexModel : PageModel
    {
        public IComponent Component { get; private set; }

        public IndexModel(ComponentRegistry components)
        {
            Component = components.FindComponent("chatty");
        }

        public void OnGet()
        {

        }
    }
}
