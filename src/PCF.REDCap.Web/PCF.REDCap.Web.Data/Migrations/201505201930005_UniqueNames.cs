namespace PCF.REDCap.Web.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueNames : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Configs", "Name", unique: true, name: "UQ_Name");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Configs", "UQ_Name");
        }
    }
}
