using ContosoComicPowerBIEmbed.Helpers;
using ContosoComicPowerBIEmbed.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace ContosoComicPowerBIEmbed.Controllers
{
    public class CampaignController : Controller
    {
        // GET: Campaign
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            try
            {
                //Fetch credentials from Azure Blob Storage
                var powerBICredential = Credentials.GetDashboardCredeials();
                
                if(powerBICredential != null)
                {
                    EmbedPowerBIReportTiles powerBIReportTilesObj = new EmbedPowerBIReportTiles();

                    //Fetching credentials from Azure Blob Stoarge
                    string powerBIUrl = ConfigurationManager.AppSettings["PowerBIUrl"];
                    string ClientId = powerBICredential.ClientId;
                    var Username = powerBICredential.Username;
                    var Password = powerBICredential.Password;
                    string AuthorityUrl = powerBICredential.AuthorityUrl;
                    string ResourceUrl = powerBICredential.ResourceUrl;
                    Guid GroupID = new Guid(powerBICredential.GroupID);
                    Guid ReportID = new Guid(powerBICredential.CampaignReportID);
                    Guid DashboardID = new Guid(powerBICredential.CampaignDashboardID);
                    string Secret = powerBICredential.Secret;

                    //Generate powerbi access token
                    var accessToken = Credentials.GenerateAccessToken();
                    var message = new Messages();

                    //Fetch Campaign Report from powerbi
                    var embedReportObj = await powerBIReportTilesObj.EmbedReport(accessToken, GroupID, ReportID);

                    //Fetch all tiles from Dashboard
                    var powerBITilesListObj = await powerBIReportTilesObj.EmbedDashboardTiles(accessToken, GroupID, DashboardID);
                    
                    //Displaying messages on UI
                    message.DisplayMessage = "";

                    //Returning view object to frontend
                    var returnViewObject = new Tuple<PowerBIReportDto, Messages, List<PowerBITileDto>>(embedReportObj, message, powerBITilesListObj);
                    return View(returnViewObject);
                }
                else
                {
                    Messages message = new Messages();
                    message.DisplayMessage = "Credentials are wrong. Please provide correct credentials to embed powerbi";
                    var returnViewObject = new Tuple<Messages>(message);
                    return View(returnViewObject);
                }
            }
            catch(Exception ex)
            {
                Messages message = new Messages();
                message.DisplayMessage = ex.StackTrace;
                var returnViewObject = new Tuple<Messages>(message);
                return View(returnViewObject);
            }
        }
    }
}