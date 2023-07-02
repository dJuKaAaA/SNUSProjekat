namespace SCADACore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TagAlarms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Priority = c.Int(nullable: false),
                        Limit = c.Double(nullable: false),
                        AnalogInput_TagName = c.String(maxLength: 128),
                        DigitalOutput_TagName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnalogInputs", t => t.AnalogInput_TagName)
                .ForeignKey("dbo.DigitalOutputs", t => t.DigitalOutput_TagName)
                .Index(t => t.AnalogInput_TagName)
                .Index(t => t.DigitalOutput_TagName);
            
            AddColumn("dbo.AnalogInputs", "Driver", c => c.Int(nullable: false));
            AddColumn("dbo.DigitalInputs", "Driver", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagAlarms", "DigitalOutput_TagName", "dbo.DigitalOutputs");
            DropForeignKey("dbo.TagAlarms", "AnalogInput_TagName", "dbo.AnalogInputs");
            DropIndex("dbo.TagAlarms", new[] { "DigitalOutput_TagName" });
            DropIndex("dbo.TagAlarms", new[] { "AnalogInput_TagName" });
            DropColumn("dbo.DigitalInputs", "Driver");
            DropColumn("dbo.AnalogInputs", "Driver");
            DropTable("dbo.TagAlarms");
        }
    }
}
