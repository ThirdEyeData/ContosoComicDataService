using ContosoComic.PowerBI.Embed.Models;
using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace ContosoComic.PowerBI.Embed.Helpers
{
    public class EmbedPowerBIReportTiles
    {
        public static string powerBIUrl = ConfigurationManager.AppSettings["PowerBIUrl"];
        public async System.Threading.Tasks.Task<List<PowerBITileDto>> EmbedDashboardTiles(TokenCredentials tokenCredentials, Guid GroupId, Guid DashboardID)
        {
            List<PowerBITileDto> tileObjs = new List<PowerBITileDto>();
            using (var powerBIClient = new PowerBIClient(new Uri(powerBIUrl), tokenCredentials))
            {
                var tileGenerateTokenRequestParameters = new GenerateTokenRequest(accessLevel: "view");
                var dashboards = await powerBIClient.Dashboards.GetDashboardsInGroupAsync(GroupId);
                var campaignDashboard = dashboards.Value.Where(x => x.Id == DashboardID).FirstOrDefault();
                if (campaignDashboard != null)
                {
                    var campaignTiles = await powerBIClient.Dashboards.GetTilesInGroupAsync(GroupId, campaignDashboard.Id);
                    foreach (var tile in campaignTiles.Value.AsParallel())
                    {
                        var tileTokenResponse = await powerBIClient.Tiles.GenerateTokenInGroupAsync(GroupId, campaignDashboard.Id, tile.Id, tileGenerateTokenRequestParameters);
                        if (tileTokenResponse != null)
                        {
                            tileObjs.Add(new PowerBITileDto { dashboardID = campaignDashboard.Id.ToString(), EmbedUrl = tile.EmbedUrl, token = tileTokenResponse, Id = tile.Id.ToString() });
                        }
                    }
                }
            }
            return tileObjs;
        }
        
        public async System.Threading.Tasks.Task<PowerBIReportDto> EmbedReport(TokenCredentials tokenCredentials, Guid GroupID, Guid ReportID)
        {
            PowerBIReportDto embedReportObj = new PowerBIReportDto();
            using (var powerBIClient = new PowerBIClient(new Uri(powerBIUrl), tokenCredentials))
            {
                var reports = await powerBIClient.Reports.GetReportsInGroupAsync(GroupID);
                if (reports.Value.Count != 0)
                {
                    var campaignReport = reports.Value.Where(x => x.Id == ReportID).FirstOrDefault();
                    if (campaignReport != null)
                    {
                        var reportGenerateTokenRequestParameters = new GenerateTokenRequest(accessLevel: "view");
                        var reportTokenResponse = await powerBIClient.Reports.GenerateTokenInGroupAsync(GroupID, campaignReport.Id, reportGenerateTokenRequestParameters);
                        if (reportTokenResponse != null)
                        {
                            embedReportObj.token = reportTokenResponse;
                            embedReportObj.EmbedUrl = campaignReport.EmbedUrl;
                            embedReportObj.ReportId = campaignReport.Id.ToString();
                        }
                    }
                }
            }
            return embedReportObj;
        }
    }
}