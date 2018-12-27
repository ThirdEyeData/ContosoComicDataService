namespace DataGenerator.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DataGenerator;

    internal sealed class Configuration : DbMigrationsConfiguration<DataGenerator.DBontext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataGenerator.DBontext context)
        {

            if (!context.ServiceFlags.Any(sf=>sf.ServiceFlagId==1))
            {
DataGenerator.ServiceFlag sf = new DataGenerator.ServiceFlag();
                sf.ServiceFlagId = 1;
                sf.ServiceFlagName = "Cloud Service Manage Flag";
                sf.FlagValue = 2;

                context.ServiceFlags.Add(sf);
            }
            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
