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
    public class InsightsController : Controller
    {
        // GET: Insights
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            EmbedPowerBIReportTiles powerBIReportTilesObj = new EmbedPowerBIReportTiles();
            try
            {
                var powerBICredential = Credentials.GetDashboardCredeials();
                if (powerBICredential != null)
                {
                    //Fetching credentials from Azure Blob Stoarge
                    string powerBIUrl = ConfigurationManager.AppSettings["PowerBIUrl"];
                    string ClientId = powerBICredential.ClientId;
                    var Username = powerBICredential.Username;
                    var Password = powerBICredential.Password;
                    string AuthorityUrl = powerBICredential.AuthorityUrl;
                    string ResourceUrl = powerBICredential.ResourceUrl;
                    Guid GroupID = new Guid(powerBICredential.GroupID);
                    Guid DahboardID = new Guid(powerBICredential.AIInsightDashboardID);
                    string Secret = powerBICredential.Secret;

                    //Generate powerbi access token
                    var accessToken = Credentials.GenerateAccessToken();
                    var message = new Messages();

                    //Fetch Decompose
                    var embedDahboardTilesObj = await powerBIReportTilesObj.EmbedDashboardTiles(accessToken, GroupID, DahboardID);

                    //Displaying messages on UI
                    message.DisplayMessage = "";

                    //Returning view object to frontend
                    var returnViewObject = new Tuple<List<PowerBITileDto>, Messages>(embedDahboardTilesObj, message);
                    return View(returnViewObject);
                }

                else
                {
                    Messages message = new Messages();
                    message.DisplayMessage = "Credentials are wrong to embed powerbi";
                    PowerBIReportDto embedReportObj = new PowerBIReportDto();
                    var embedDahboardTilesObj = new List<PowerBITileDto>();
                    var returnViewObject = new Tuple<PowerBIReportDto, Messages, List<PowerBITileDto>>(embedReportObj, message, embedDahboardTilesObj);
                    return View(returnViewObject);
                }
            }
            catch (Exception ex)
            {
                Messages message = new Messages();
                message.DisplayMessage = ex.StackTrace;
                PowerBIReportDto embedReportObj = new PowerBIReportDto();
                var embedDahboardTilesObj = new List<PowerBITileDto>();
                var returnViewObject = new Tuple<PowerBIReportDto, Messages, List<PowerBITileDto>>(embedReportObj, message, embedDahboardTilesObj);
                return View(returnViewObject);
            }
        }
    }
}