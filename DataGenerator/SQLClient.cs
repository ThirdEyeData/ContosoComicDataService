using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataGenerator
{
    static class SQLClient
    {
        public static string SQLConnectionString { get; set; }
        public static int MarketingCost { get; set; }

        public static SqlConnection sqlConn;

        public static void OpenConnection(string connectionString)
        {
            SQLConnectionString = connectionString;
            sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
        }

        public static void CloseConnection()
        {

            sqlConn.Close();
        }

        public static SummaryData GetData()
        {
            SqlCommand sqlCommand;
            SqlDataReader sqlDataReader;
            int TotalVisits = 0;
            int TotalAcquisitions = 0;
            int TotalRevenue = 0;
            int RevenuePerAcquisition = 0;
            int ConversionRate = 0;
            SummaryData summaryData = new SummaryData();

            string query = "Select count(VisitId) from Visits";

            sqlCommand = new SqlCommand(query, sqlConn);
            sqlDataReader = sqlCommand.ExecuteReader();

            while(sqlDataReader.Read())
            {
                TotalVisits = Convert.ToInt32( sqlDataReader.GetValue(0));
            }

            sqlDataReader.Close();
            query = "Select count(VisitId), sum(Amount) from Visits where Purchased = 1";

            sqlCommand = new SqlCommand(query, sqlConn);
            sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                TotalAcquisitions = Convert.ToInt32(sqlDataReader.GetValue(0));
                TotalRevenue = Convert.ToInt32(sqlDataReader.GetValue(1));
            }
            sqlDataReader.Close();



            if (TotalAcquisitions>0)
            {                

                RevenuePerAcquisition = Convert.ToInt32((decimal)TotalRevenue / TotalAcquisitions);
            }
            

            

            decimal ROI = ((decimal)(TotalRevenue - MarketingCost) / MarketingCost);

            if (TotalVisits>0)
            {
                ConversionRate = (int)(((decimal)TotalAcquisitions / TotalVisits) * 100);
            }

            summaryData.TotalVisits = TotalVisits;
            summaryData.ConversionRate = ConversionRate;
            summaryData.Revenue = TotalRevenue;
            summaryData.RevenuePerAcquisition = RevenuePerAcquisition;
            summaryData.ROI = ROI;
            summaryData.TotalAcquisitions = TotalAcquisitions;

            return summaryData;
        }

    }

    public class SummaryData
    {
        public int TotalVisits { get; set; }
        public int TotalAcquisitions { get; set; }
        public int Revenue { get; set; }
        public decimal ROI { get; set; }
        public int RevenuePerAcquisition { get; set; }
        public int ConversionRate { get; set; }
    }
}
