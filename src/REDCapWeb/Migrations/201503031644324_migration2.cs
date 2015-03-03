namespace REDCapWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserStudies", "ApiKey", c => c.String(nullable: false));
            AlterColumn("dbo.UserStudies", "StudyName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserStudies", "StudyName", c => c.String());
            AlterColumn("dbo.UserStudies", "ApiKey", c => c.String());
        }
    }
}
