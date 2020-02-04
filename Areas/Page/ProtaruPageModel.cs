using System;
using Itm.Misc;

namespace Protaru.Areas.Page
{
    public class ProtaruPageModel : CustomPageModel, ILayoutPageModel, IActiveMenuPageModel
    {
        public string Layout { get; set; } = String.Empty;

        public ActiveMenu ActiveMenu { get; set; }
    }
}