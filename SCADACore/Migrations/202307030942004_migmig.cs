namespace SCADACore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migmig : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TagAlarms", "DigitalInput_TagName", "dbo.DigitalInputs");
            DropIndex("dbo.TagAlarms", new[] { "DigitalInput_TagName" });
            AddColumn("dbo.TagAlarms", "AlarmName", c => c.String());
            DropColumn("dbo.TagAlarms", "DigitalInput_TagName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TagAlarms", "DigitalInput_TagName", c => c.String(maxLength: 128));
            DropColumn("dbo.TagAlarms", "AlarmName");
            CreateIndex("dbo.TagAlarms", "DigitalInput_TagName");
            AddForeignKey("dbo.TagAlarms", "DigitalInput_TagName", "dbo.DigitalInputs", "TagName");
        }
    }
}
