using ContosoComicPowerBIEmbed.Models;
using Microsoft.Rest;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ContosoComicPowerBIEmbed.Helpers
{
    public class Credentials
    {
        static CloudBlobClient strclient = null;
        static CloudBlobContainer container = null;
        static CloudStorageAccount storageAccount = null;

        public static PowerBICredential GetDashboardCredeials()
        {
            try
            {
                string containerName = ConfigurationManager.AppSettings["Container"];
                string accountName = ConfigurationManager.AppSettings["AccountName"];
                string accountKey = ConfigurationManager.AppSettings["AccountKey"];
                string fileName = ConfigurationManager.AppSettings["CredentialFile"];
                storageAccount = new CloudStorageAccount(new StorageCredentials(accountName, accountKey), true);
                strclient = storageAccount.CreateCloudBlobClient();
                container = strclient.GetContainerReference(containerName);
                CloudBlockBlob blob = container.GetBlockBlobReference($"{fileName}");
                string contents = blob.DownloadTextAsync().Result;
                var fileContentJsonObj = JsonConvert.DeserializeObject<PowerBICredential>(contents);
                return fileContentJsonObj;
            }
            catch(Exception ex)
            {
                PowerBICredential powerBICredential = null;
                return powerBICredential;
            }
        }

        /// <summary>
        /// This methos is here to generate Bearer Access Token for embedding PowerBI
        /// To run this method, mainly we need parameters like ClientId, Username, Password, AuthorityUrl, ResourceUrl and Secret
        /// </summary>
        /// <returns></returns>
        public static TokenCredentials GenerateAccessToken()
        {
            try
            {
                PowerBICredential powerBICredential = new PowerBICredential();
                powerBICredential = Credentials.GetDashboardCredeials();

                if(powerBICredential != null)
                {
                    string ClientId = powerBICredential.ClientId;
                    var Username = powerBICredential.Username;
                    var Password = powerBICredential.Password;
                    string AuthorityUrl = powerBICredential.AuthorityUrl;
                    string ResourceUrl = powerBICredential.ResourceUrl;
                    string Secret = powerBICredential.Secret;

                    var formcontent = new FormUrlEncodedContent(new[]
                    {   
                        new KeyValuePair<String,String>("grant_type","password"),
                        new KeyValuePair<String,String>("scope","openid"),
                        new KeyValuePair<String,String>("resource",ResourceUrl),
                        new KeyValuePair<String,String>("client_id",ClientId),
                        new KeyValuePair<String,String>("client_secret",Secret),
                        new KeyValuePair<String,String>("username",Username),
                        new KeyValuePair<String,String>("password",Password)
                    });
                        HttpClient client = new HttpClient();
                        HttpResponseMessage response = client.PostAsync(AuthorityUrl, formcontent).Result;
                        response.EnsureSuccessStatusCode();
                        var accesstoken = JsonConvert.DeserializeObject<AccessToken>(response.Content.ReadAsStringAsync().Result).access_token;
                        var tokenCredentials = new TokenCredentials(accesstoken, "Bearer");
                        return tokenCredentials;
                }
                else
                {
                    TokenCredentials tokenCredentials = null;
                    return tokenCredentials;
                }
                
            }
            catch(Exception ex)
            {
                TokenCredentials tokenCredentials = null;
                return tokenCredentials;
            }
            
        } 
    }
}