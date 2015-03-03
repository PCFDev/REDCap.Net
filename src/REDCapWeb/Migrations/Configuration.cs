namespace REDCapWeb.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<REDCapWeb.UserStudyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(REDCapWeb.UserStudyContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.UserStudies.AddOrUpdate(
                p => p.Id,
                new UserStudy
                {
                    Id = 1,
                    ApiKey = "820E65DD2D930A0859BB3F727989D29E",
                    KeyFieldName = "study_id",
                    KeyFormName = "demographics",
                    LastUpdated = new DateTime(2015, 3, 3,9,12,22),
                    Created = new DateTime(2015,1,1,12,22,30),
                    StudyName = "Study 2",
                    UserName = "alfred",
                    RedCapUrl="https://redcap.wustl.edu/redcap/srvrs/dev_v3_1_0_001/redcap/api/"
                },
                new UserStudy
                {
                    Id=2,
                    ApiKey="065C7F9FE54C3D2793304136DD7991B3",
                    KeyFormName="",
                    KeyFieldName="",
                    LastUpdated=new DateTime(2015, 3,1,11,22,35),
                    Created = new DateTime(2015,1,1,12,22,30),
                    StudyName="Study 1",
                    UserName="alfred",
                    RedCapUrl="https://redcap.wustl.edu/redcap/srvrs/dev_v3_1_0_001/redcap/api/"
                });
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
