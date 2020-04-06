using Microsoft.PowerBI.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoComic.PowerBI.Embed.Models
{
    public class PowerBITileDto
    {
        public EmbedToken token { get; set; }
        public string dashboardID { get; set; }
        public string EmbedUrl { get; set; }
        public string Id { get; set; }
    }
}