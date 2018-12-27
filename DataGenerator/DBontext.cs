namespace DataGenerator
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.PowerBI.Api;

    public class DBontext : DbContext
    {
        // Your context has been configured to use a 'DBontext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DataGenerator.DBontext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DBontext' 
        // connection string in the application configuration file.
        public DBontext()
            : base("name=comicdb")
        {
            
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Visit> Visits { get; set; }
        public virtual DbSet<ServiceFlag> ServiceFlags { get; set; }
    }

    public class DBInitializer<T> : DropCreateDatabaseAlways<DBontext>
    {
        protected override void Seed(DBontext context)
        {
            base.Seed(context);

            
            
        }
    }

    public class Visit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VisitId { get; set; }
        public DateTime VisitDate { get; set; }
        public string TrafficSource { get; set; }
        public Int16 SessionDuration { get; set; }
        public Int16 CountOfPages { get; set; }
        public bool Purchased { get; set; }
        public float Amount { get; set; }
        public string VisitorType { get; set; }
    }

    public class ServiceFlag
    {
        [Key]
        public int ServiceFlagId { get; set; }
        public string ServiceFlagName { get; set; }
        public int FlagValue { get; set; }
        public string CampaignName { get; set; }
        public decimal ROI { get; set; }
        public int CampaignId { get; set; }
    }
}