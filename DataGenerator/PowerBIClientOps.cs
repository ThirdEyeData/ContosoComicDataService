using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Diagnostics;
using System.Diagnostics;

namespace DataGenerator
{
    static class PowerBIClientOps
    {
        public static string AccessToken { get; set; }
        public static string DatasetId { get; set; }
        public static string DatasetName { get; set; }
        public static string TableName { get; set; }
        public static string ClientID { get; set; }
        public static string RedirectUri { get; set; }
        public static string ResourceUri { get; set; }
        public static string AuthorityUri { get; set; }
        public static string UserName { get; set; }
        public static string Password { get; set; }            
        public static string SummaryTableName { get; set; }
        public static string GroupId { get; set; }

        public static string Gettoken()
        {
            AuthenticationContext authContext = new AuthenticationContext(AuthorityUri);
            var result = authContext.AcquireTokenAsync(ResourceUri, ClientID, new UserPasswordCredential(UserName, Password)).Result;

            string token = string.Empty;
            if(result.AccessToken != null && result.AccessToken != string.Empty)
            {
                token = result.AccessToken;

                AccessToken = token;
            }
            return token;
        }

        #region Create a dataset in Power BI
        public static void CreateDataset()
        {
            //TODO: Add using System.Net and using System.IO

            string powerBIDatasetsApiUrl = string.Format("https://api.powerbi.com/v1.0/myorg/groups/{0}/datasets",GroupId);
            //POST web request to create a dataset.
            //To create a Dataset in a group, use the Groups uri: https://api.PowerBI.com/v1.0/myorg/groups/{group_id}/datasets
            HttpWebRequest request = System.Net.WebRequest.Create(powerBIDatasetsApiUrl) as System.Net.HttpWebRequest;
            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentLength = 0;
            request.ContentType = "application/json";

            //Add token to the request header
            request.Headers.Add("Authorization", String.Format("Bearer {0}", AccessToken));


            //Create dataset JSON for POST request
            string datasetJson = "{\"name\": \"" + DatasetName + "\", \"tables\": " +
                "[{\"name\": \"" + TableName + "\", \"columns\": " +
                "[{ \"name\": \"VisitDate\", \"dataType\": \"DateTime\"}, " +
                "{ \"name\": \"TrafficSource\", \"dataType\": \"String\"}, " +
                "{ \"name\": \"SessionDuration\", \"dataType\": \"Int64\"}," +
                "{ \"name\": \"Purchased\", \"dataType\": \"Boolean\"}," +
                "{ \"name\": \"CountOfPages\", \"dataType\": \"Int64\"}," +
                "{ \"name\": \"Amount\", \"dataType\": \"Int64\"}," +
                "{ \"name\": \"VisitorType\", \"dataType\": \"String\"}" +
                "]}," +
                "{\"name\": \"" + SummaryTableName + "\", \"columns\":" +
                "[{ \"name\": \"TotalVisits\", \"dataType\": \"Int64\"}, " +
                "{ \"name\": \"TotalAcquisitions\", \"dataType\": \"Int64\"}, " +
                "{ \"name\": \"Revenue\", \"dataType\": \"Int64\"}, " +
                "{ \"name\": \"ROI\", \"dataType\": \"Decimal\"}," +
                "{ \"name\": \"RevenuePerAcquisition\", \"dataType\": \"Int64\"}," +
                "{ \"name\": \"ConversionRate\", \"dataType\": \"Int64\"}" +
                "]}" +
                "]}";

            //POST web request
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(datasetJson);
            request.ContentLength = byteArray.Length;

            //Write JSON byte[] into a Stream
            using (Stream writer = request.GetRequestStream())
            {
                writer.Write(byteArray, 0, byteArray.Length);

                var response = (HttpWebResponse)request.GetResponse();

                Console.WriteLine(string.Format("Dataset {0}", response.StatusCode.ToString()));

                Console.ReadLine();
            }
        }
        #endregion

        #region Get a dataset to add rows into a Power BI table
        public static void GetDataset()
        {
            //string powerBIDatasetsApiUrl = "https://api.powerbi.com/v1.0/myorg/datasets";
            string powerBIDatasetsApiUrl = string.Format("https://api.powerbi.com/v1.0/myorg/groups/{0}/datasets",GroupId);
            //POST web request to create a dataset.
            //To create a Dataset in a group, use the Groups uri: https://api.PowerBI.com/v1.0/groups/{group_id}/datasets
            HttpWebRequest request = System.Net.WebRequest.Create(powerBIDatasetsApiUrl) as System.Net.HttpWebRequest;
            request.KeepAlive = true;
            request.Method = "GET";
            request.ContentLength = 0;
            request.ContentType = "application/json";

            //Add token to the request header
            request.Headers.Add("Authorization", String.Format("Bearer {0}", AccessToken));

            string datasetId = string.Empty;
            //Get HttpWebResponse from GET request
            using (HttpWebResponse httpResponse = request.GetResponse() as System.Net.HttpWebResponse)
            {
                //Get StreamReader that holds the response stream
                using (StreamReader reader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
                {
                    string responseContent = reader.ReadToEnd();

                    //TODO: Install NuGet Newtonsoft.Json package: Install-Package Newtonsoft.Json
                    //and add using Newtonsoft.Json
                    var results = JsonConvert.DeserializeObject<dynamic>(responseContent);

                    //Get the first id


                    for (int i = 0; i < results["value"].Count; i++)
                    {
                        if (results["value"][i]["name"] == DatasetName)
                        {
                            datasetId = results["value"][i]["id"];
                            DatasetId = datasetId;
                            break;
                        }
                    }

                    Console.WriteLine(String.Format("Dataset ID: {0}", datasetId));
                    Console.ReadLine();

                   
                }
            }
        }
        #endregion

        #region Add rows to a Power BI table
        //public static void AddRows(List<VisitNew> visits, SummaryData summaryData)
        public static void AddRows(List<VisitNew> visits)
        {
            string powerBIApiAddRowsUrl = String.Format("https://api.powerbi.com/v1.0/myorg/groups/{2}/datasets/{0}/tables/{1}/rows", DatasetId, TableName,GroupId);
            string powerBIApiAddSummaryRowsUrl = String.Format("https://api.powerbi.com/v1.0/myorg/groups/{2}/datasets/{0}/tables/{1}/rows", DatasetId, SummaryTableName,GroupId);

            TableRows rows = new TableRows();
            rows.rows = visits;

            SummaryTableRows summaryRows = new SummaryTableRows();
            //summaryRows.rows.Add(summaryData);

            //JSON content for product row
            string rowsJson = Newtonsoft.Json.JsonConvert.SerializeObject(rows);
            string summaryRowsJson = Newtonsoft.Json.JsonConvert.SerializeObject(summaryRows);

            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", AccessToken));
                HttpContent content = new StringContent(rowsJson);
                HttpResponseMessage response = httpClient.PostAsync(powerBIApiAddRowsUrl, content).Result;
                response.EnsureSuccessStatusCode();

                //HttpContent summaryContent = new StringContent(summaryRowsJson);
                //HttpResponseMessage summaryResponse = httpClient.PostAsync(powerBIApiAddSummaryRowsUrl, summaryContent).Result;
                //summaryResponse.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {

                Trace.TraceError(ex.Message);
            }
            

        }

        #endregion

        #region Delete rows of Power BI Table
        public static void DeleteRows(string tableName)
        {
            string powerBIApiDeleteRowsUrl = String.Format("https://api.powerbi.com/v1.0/myorg/groups/{2}/datasets/{0}/tables/{1}/rows", DatasetId, tableName,GroupId);

            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", AccessToken));
                HttpResponseMessage response = httpClient.DeleteAsync(powerBIApiDeleteRowsUrl).Result;
                response.EnsureSuccessStatusCode();                
            }
            catch (Exception ex)
            {

                Trace.TraceError(ex.Message);
            }
        }
        #endregion
    }

    public class TableRows
    {
        public List<VisitNew> rows { get; set; }

        public TableRows()
        {
            rows = new List<VisitNew>();
        }
    }

    public class SummaryTableRows
    {
        public List<SummaryData> rows { get; set; }

        public SummaryTableRows()
        {
            rows = new List<SummaryData>();
        }
    }
}
