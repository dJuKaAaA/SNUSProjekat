namespace SCADACore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class miggy : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TagAlarms", "DigitalOutput_TagName", "dbo.DigitalOutputs");
            DropIndex("dbo.TagAlarms", new[] { "DigitalOutput_TagName" });
            DropColumn("dbo.TagAlarms", "DigitalOutput_TagName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TagAlarms", "DigitalOutput_TagName", c => c.String(maxLength: 128));
            CreateIndex("dbo.TagAlarms", "DigitalOutput_TagName");
            AddForeignKey("dbo.TagAlarms", "DigitalOutput_TagName", "dbo.DigitalOutputs", "TagName");
        }
    }
}
