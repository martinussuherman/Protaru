using Itm.Misc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MonevAtr.Models;
using Newtonsoft.Json;
using Syncfusion.EJ2.Maps;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonevAtr.Pages
{
    public class RtrIndexModel : CustomPageModel
    {
        public RtrIndexModel(
            PomeloDbContext context,
            IConfiguration configuration)
        {
            _configuration = configuration;
            selectListUtilities = new SelectListUtilities(context);
        }

        public int StatusYear { get; set; }

        public int StatusMonth { get; set; }

        public string BingKey => _configuration.GetValue<string>("BingKey");

        public object MapsMarkerData => ReadMarkerFromFile();

        public MapsCenterPosition CenterPosition => new MapsCenterPosition
        {
            Latitude = -2.8,
            Longitude = 118
        };

        public MapsMarkerClusterSettings ClusterSettings => new MapsMarkerClusterSettings
        {
            AllowClusterExpand = true,
            AllowClustering = true,
            Shape = MarkerType.Image,
            Height = 40,
            Width = 40,
            ImageUrl = Url.Content("~/cluster.svg"),
            LabelStyle = new MapsFont
            {
                Color = "White"
            }
        };

        public List<MapsMarker> MarkerSettings => new List<MapsMarker>
        {
            new MapsMarker
            {
                AnimationDuration=0,
                DataSource=MapsMarkerData,
                Fill = "#ff0000",
                Height=10,
                Shape = MarkerType.Circle,
                TooltipSettings=_tooltipSettings,
                Visible=true,
                Width=10
            }
        };

        public MapsZoomSettings ZoomSettings => new MapsZoomSettings
        {
            Enable = true,
            HorizontalAlignment = Alignment.Near,
            PinchZooming = true,
            ShouldZoomInitially = false,
            Toolbars = _toolbars,
            ToolBarOrientation = Orientation.Vertical,
            ZoomFactor = 5
        };


        public async Task<IActionResult> OnGetAsync()
        {
            StatusYear = DateTime.Today.Year;
            StatusMonth = DateTime.Today.Month;
            ViewData["TahunLintasSektorDanPersetujuanSubstansi"] =
                await selectListUtilities.TahunRapatLintasSektorDanPersetujuanSubstansi();
            return Page();
        }

        private object ReadMarkerFromFile()
        {
            string allText = System.IO.File.ReadAllText("./wwwroot/markercluster.js");
            return JsonConvert.DeserializeObject(allText);
        }

        private readonly string[] _toolbars = new string[]
        {
            "Zoom",
            "ZoomIn",
            "ZoomOut",
            "Pan",
            "Reset"
        };
        private readonly MapsTooltipSettings _tooltipSettings = new MapsTooltipSettings
        {
            // Template = "#tooltip-template",
            Visible = true,
            ValuePath = "city"
        };
        private readonly IConfiguration _configuration;
        private readonly SelectListUtilities selectListUtilities;
    }
}
