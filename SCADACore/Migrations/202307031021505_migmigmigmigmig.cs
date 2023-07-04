namespace SCADACore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migmigmigmigmig : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TagAlarms", name: "AnalogInput_TagName", newName: "InputTagName");
            RenameIndex(table: "dbo.TagAlarms", name: "IX_AnalogInput_TagName", newName: "IX_InputTagName");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TagAlarms", name: "IX_InputTagName", newName: "IX_AnalogInput_TagName");
            RenameColumn(table: "dbo.TagAlarms", name: "InputTagName", newName: "AnalogInput_TagName");
        }
    }
}
