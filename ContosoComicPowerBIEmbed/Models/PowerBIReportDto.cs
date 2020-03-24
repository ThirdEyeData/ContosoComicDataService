using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.PowerBI.Api.Models;

namespace ContosoComicPowerBIEmbed.Models
{
    public class PowerBIReportDto
    {
        public EmbedToken token { get; set; }
        public string EmbedUrl { get; set; }
        public string ReportId { get; set; }
    }
}