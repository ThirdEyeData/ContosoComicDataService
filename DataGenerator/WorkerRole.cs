using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.PowerBI.Api.V2;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.IO;

namespace DataGenerator
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);
        
        private RandomNumberGenerator randomNumberGenerator = new RandomNumberGenerator();
        static HttpClient httpClient= new HttpClient();
        static string PushURL;
        private string datasetid = string.Empty;
        private string accesstoken = string.Empty;
        public enum ServiceCommand
        {
            Start = 1,
            Stop = 2,
            Pause = 3,
            Restart = 4,
            Clear = 5
        }

        public override void Run()
        {
            Trace.TraceInformation("DataGenerator is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at https://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();

            Trace.TraceInformation("DataGenerator has been started");

            PowerBIClientOps.ClientID = RoleEnvironment.GetConfigurationSettingValue("ClientID");
            PowerBIClientOps.AuthorityUri = RoleEnvironment.GetConfigurationSettingValue("AuthorityUri");
            PowerBIClientOps.DatasetName = RoleEnvironment.GetConfigurationSettingValue("DatasetName");
            PowerBIClientOps.TableName = RoleEnvironment.GetConfigurationSettingValue("TableName");
            PowerBIClientOps.SummaryTableName = RoleEnvironment.GetConfigurationSettingValue("SummaryTableName");
            PowerBIClientOps.RedirectUri = RoleEnvironment.GetConfigurationSettingValue("RedirectUri");
            PowerBIClientOps.ResourceUri = RoleEnvironment.GetConfigurationSettingValue("ResourceUri");
            PowerBIClientOps.UserName = RoleEnvironment.GetConfigurationSettingValue("UserName");
            PowerBIClientOps.Password = RoleEnvironment.GetConfigurationSettingValue("Password");
            PowerBIClientOps.GroupId = RoleEnvironment.GetConfigurationSettingValue("GroupId");

            PushURL = RoleEnvironment.GetConfigurationSettingValue("PowerBIPushURL");

            SQLClient.OpenConnection(RoleEnvironment.GetConfigurationSettingValue("SQLConnectionString"));
            SQLClient.MarketingCost = Convert.ToInt32( RoleEnvironment.GetConfigurationSettingValue("MarketingCost"));
            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("DataGenerator is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();
            SQLClient.CloseConnection();
            Trace.TraceInformation("DataGenerator has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following with your own logic.
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    int ServiceFlagValue;
                    using (DBontext dbcontext = new DBontext())
                    {
                        ServiceFlag serviceFlag = dbcontext.ServiceFlags.FirstOrDefault();
                        ServiceFlagValue = serviceFlag.FlagValue;
                    }

                    switch (ServiceFlagValue)
                    {
                        case (int)ServiceCommand.Start:
                            IngestData();
                            break;
                        case (int)ServiceCommand.Stop:
                            break;
                        case (int)ServiceCommand.Restart:
                            ClearDataset();
                            SetServiceFlag((int)ServiceCommand.Start);
                            break;
                        case (int)ServiceCommand.Clear:
                            ClearDataset();
                            SetServiceFlag((int)ServiceCommand.Stop);
                            break;
                    }

                    Thread.Sleep(2000);
                }
                catch (Exception ex)
               {

                    Console.WriteLine(ex.Message);
                }
                
            }
        }

        public void IngestData()
        {
            int visitCount = randomNumberGenerator.GetNextVisitCount();
            DateTime currentDate = DateTime.Now;

            List<Visit> visits = new List<Visit>();
            List<VisitNew> newvisits = new List<VisitNew>();

            for (int i = 0; i < visitCount; i++)
            {
                Visit visit = new Visit();
                bool IsPurchased = randomNumberGenerator.IsPurchased();
                int Amount = 0;

                if(IsPurchased)
                {
                    Amount = randomNumberGenerator.GetAmount();
                }

                string TrafficSource = randomNumberGenerator.GetTrafficSource();

                int SessionDuration = randomNumberGenerator.GetSessionDuration();

                int PageCount = randomNumberGenerator.GetPageCount(SessionDuration);

                string CustomerType = randomNumberGenerator.GetCustomerType();

                visit.VisitDate = currentDate;
                visit.TrafficSource = TrafficSource;
                visit.SessionDuration = (short)SessionDuration;
                visit.CountOfPages = (short)PageCount;
                visit.Purchased = IsPurchased;
                visit.Amount = (short)Amount;
                visit.VisitorType = CustomerType;

                visits.Add(visit);

                VisitNew visitNew = new VisitNew();

                visitNew.VisitDate = currentDate;
                visitNew.TrafficSource = TrafficSource;
                visitNew.SessionDuration = (short)SessionDuration;
                visitNew.CountOfPages = (short)PageCount;
                visitNew.Purchased = IsPurchased;
                visitNew.Amount = (short)Amount;
                visitNew.VisitorType = CustomerType;

                newvisits.Add(visitNew);
            }

            InitializePowerBIOps();
            //using (DBontext dbContext = new DBontext())
            //{
            //    dbContext.Visits.AddRange(visits);
            //    dbContext.SaveChanges();
            //}

            //SummaryData summaryData = SQLClient.GetData();
            //PowerBIClientOps.DeleteRows(PowerBIClientOps.SummaryTableName);

            VisitStreamCount visitStreamCount = new VisitStreamCount();
            visitStreamCount.VisitDate = currentDate;
            visitStreamCount.VisitCount = newvisits.Count;

            List<VisitStreamCount> visitStreamCounts = new List<VisitStreamCount>();
            visitStreamCounts.Add(visitStreamCount);
            var response = HttpPostAsync(PushURL, JsonConvert.SerializeObject(visitStreamCounts));
            PowerBIClientOps.AddRows(newvisits);

        }

        public void SetServiceFlag(int FlagValue)
        {
            
            

            using (DBontext dbContext = new DBontext())
            {
                ServiceFlag sf = dbContext.ServiceFlags.FirstOrDefault();
                sf.FlagValue = FlagValue;
                dbContext.Entry<ServiceFlag>(sf).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }

        }

        public void ClearDataset()
        {
            //using (DBontext dbContext = new DBontext())
            //{
            //    dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE [Visits]");
            //}

            InitializePowerBIOps();

            PowerBIClientOps.DeleteRows(PowerBIClientOps.TableName);
        }

        static async Task<HttpResponseMessage> HttpPostAsync(string url, string data)
        {
            // Construct an HttpContent object from StringContent
            HttpContent content = new StringContent(data);
            HttpResponseMessage response = await httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return response;
        }    

        public void InitializePowerBIOps()
        {
            if (PowerBIClientOps.AccessToken == string.Empty || PowerBIClientOps.AccessToken == null)
            {
                PowerBIClientOps.Gettoken();
            }

            if (PowerBIClientOps.DatasetId == string.Empty || PowerBIClientOps.DatasetId == null)
            {
                PowerBIClientOps.GetDataset();

                if (PowerBIClientOps.DatasetId == string.Empty | PowerBIClientOps.DatasetId == null)
                {
                    PowerBIClientOps.CreateDataset();
                    PowerBIClientOps.GetDataset();
                }
            }
        }
       
    } 
   


    public class VisitNew
    {
        
        public DateTime VisitDate { get; set; }
        public string TrafficSource { get; set; }
        public Int16 SessionDuration { get; set; }
        public Int16 CountOfPages { get; set; }
        public bool Purchased { get; set; }
        public Int16 Amount { get; set; }
        public string VisitorType { get; set; }
    }

    public class VisitStreamCount
    {
        public DateTime VisitDate { get; set; }
        public int VisitCount { get; set; }
    }
}
