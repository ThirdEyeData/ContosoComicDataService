using ContosoComic.PowerBI.Embed.Helpers;
using ContosoComic.PowerBI.Embed.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoComic.PowerBI.Embed.Controllers
{
    public class CampaignsController : Controller
    {
        // GET: Campaigns
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            try
            {
                //Fetch credentials from Azure Blob Storage
                var powerBICredential = Credentials.GetDashboardCredeials();

                if (powerBICredential != null)
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
                    Guid DashboardID = new Guid(powerBICredential.CampaignsDashboardID);
                    string Secret = powerBICredential.Secret;

                    //Generate powerbi access token
                    var accessToken = Credentials.GenerateAccessToken();
                    var message = new Messages();

                    //Fetch all tiles from Dashboard
                    var powerBITilesListObj = await powerBIReportTilesObj.EmbedDashboardTiles(accessToken, GroupID, DashboardID);

                    //Displaying messages on UI
                    message.DisplayMessage = "";
                    PowerBIStaticData.PowerBITiles = powerBITilesListObj;
                    //Returning view object to frontend
                    var returnViewObject = new Tuple<Messages, List<PowerBITileDto>>(message, powerBITilesListObj);
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
            catch (Exception ex)
            {
                Messages message = new Messages();
                message.DisplayMessage = ex.StackTrace;
                var returnViewObject = new Tuple<Messages>(message);
                return View(returnViewObject);
            }
        }
    }
}