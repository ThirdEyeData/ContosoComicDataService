using ContosoComicPowerBIEmbed.Helpers;
using ContosoComicPowerBIEmbed.Models;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace ContosoComicPowerBIEmbed.Controllers
{
    public class FinancialsController : Controller
    {
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            EmbedPowerBIReportTiles powerBIReportTilesObj = new EmbedPowerBIReportTiles();
            try
            {
                //Fetch credentials from Azure Blob Storage
                var powerBICredential = Credentials.GetDashboardCredeials();
                
                if(powerBICredential != null)
                {
                    //Fetching credentials from Azure Blob Stoarge
                    string powerBIUrl = ConfigurationManager.AppSettings["PowerBIUrl"];
                    string ClientId = powerBICredential.ClientId;
                    var Username = powerBICredential.Username;
                    var Password = powerBICredential.Password;
                    string AuthorityUrl = powerBICredential.AuthorityUrl;
                    string ResourceUrl = powerBICredential.ResourceUrl;
                    Guid GroupID = new Guid(powerBICredential.GroupID);
                    Guid ReportID = new Guid(powerBICredential.FinanceReportID);
                    string Secret = powerBICredential.Secret;

                    //Generate powerbi access token
                    var accessToken = Credentials.GenerateAccessToken();
                    var message = new Messages();
                    
                    //Fetch Finanacial report from PowerBI
                    var embedReportObj = await powerBIReportTilesObj.EmbedReport(accessToken, GroupID, ReportID);
                    
                    //Displaying messages on UI
                    message.DisplayMessage = "";
                    
                    //Returning view object to frontend
                    var returnViewObject = new Tuple<PowerBIReportDto, Messages>(embedReportObj, message);
                    return View(returnViewObject);
                }
                else
                {
                    Messages message = new Messages();
                    message.DisplayMessage = "Credentials are wrong to embed powerbi";
                    PowerBIReportDto embedReportObj = new PowerBIReportDto();
                    var returnViewObject = new Tuple<PowerBIReportDto, Messages>(embedReportObj, message);
                    return View(returnViewObject);
                }
            }
            catch(Exception ex)
            {
                Messages message = new Messages();
                message.DisplayMessage = ex.StackTrace;
                PowerBIReportDto embedReportObj = new PowerBIReportDto();
                var returnViewObject = new Tuple<PowerBIReportDto, Messages>(embedReportObj, message);
                return View(returnViewObject);
            }
        }
    }
}