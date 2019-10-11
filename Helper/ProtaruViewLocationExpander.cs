using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;

namespace MonevAtr
{
    public class ProtaruViewLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values["customviewlocation"] = nameof(ProtaruViewLocationExpander);
        }

        public IEnumerable<string> ExpandViewLocations(
            ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            List<string> result = new List<string>(viewLocations)
            {
                "~/Pages/Shared/Rtr/{1}/{0}.cshtml",
                "~/Pages/Shared/Rtr/{0}.cshtml"
            };

            return result;
        }
    }
}