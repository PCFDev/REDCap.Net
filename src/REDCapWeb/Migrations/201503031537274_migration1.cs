namespace REDCapWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserStudies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KeyFieldName = c.String(),
                        KeyFormName = c.String(),
                        UserName = c.String(),
                        LastUpdated = c.DateTime(nullable: false),
                        ApiKey = c.String(),
                        StudyName = c.String(),
                        RedCapUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserStudies");
        }
    }
}
