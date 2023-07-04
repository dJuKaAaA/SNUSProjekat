namespace SCADACore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class miggymiggym : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlarmReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlarmId = c.Int(nullable: false),
                        Timestamp = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TagAlarms", t => t.AlarmId, cascadeDelete: true)
                .Index(t => t.AlarmId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlarmReports", "AlarmId", "dbo.TagAlarms");
            DropIndex("dbo.AlarmReports", new[] { "AlarmId" });
            DropTable("dbo.AlarmReports");
        }
    }
}
