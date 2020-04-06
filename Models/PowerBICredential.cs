using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoComic.PowerBI.Embed.Models
{
    public class PowerBICredential
    {
        public string ClientId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ResourceUrl { get; set; }
        public string GroupID { get; set; }
        public string FinanceReportID { get; set; }
        public string CampaignReportID { get; set; }
        public string CampaignDashboardID { get; set; }
        public string Secret { get; set; }
        public string AuthorityUrl { get; set; }
        public string AIInsightReportID { get; set; }
        public string AIInsightDashboardID { get; set; }
        public string Campaign1DashbaordID { get; set; }
        public string Campaign2DashbaordID { get; set; }
        public string Campaign3DashbaordID { get; set; }
        public string CampaignsDashboardID { get; set; }
    }
}