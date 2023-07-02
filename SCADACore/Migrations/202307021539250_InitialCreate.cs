namespace SCADACore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnalogInputs",
                c => new
                    {
                        TagName = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        IOAddress = c.Int(nullable: false),
                        ScanTime = c.Int(nullable: false),
                        OnScan = c.Boolean(nullable: false),
                        Value = c.Double(nullable: false),
                        LowLimit = c.Double(nullable: false),
                        HighLimit = c.Double(nullable: false),
                        Units = c.String(),
                    })
                .PrimaryKey(t => t.TagName);
            
            CreateTable(
                "dbo.AnalogOutputs",
                c => new
                    {
                        TagName = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        IOAddress = c.Int(nullable: false),
                        Value = c.Double(nullable: false),
                        LowLimit = c.Double(nullable: false),
                        HighLimit = c.Double(nullable: false),
                        Units = c.String(),
                    })
                .PrimaryKey(t => t.TagName);
            
            CreateTable(
                "dbo.DigitalInputs",
                c => new
                    {
                        TagName = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        IOAddress = c.Int(nullable: false),
                        ScanTime = c.Int(nullable: false),
                        OnScan = c.Boolean(nullable: false),
                        Value = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TagName);
            
            CreateTable(
                "dbo.DigitalOutputs",
                c => new
                    {
                        TagName = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        IOAddress = c.Int(nullable: false),
                        Value = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TagName);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DigitalOutputs");
            DropTable("dbo.DigitalInputs");
            DropTable("dbo.AnalogOutputs");
            DropTable("dbo.AnalogInputs");
        }
    }
}
