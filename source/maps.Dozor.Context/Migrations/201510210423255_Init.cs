namespace maps.Dozor.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        GpsId = c.Int(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Markers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BusId = c.Int(nullable: false),
                        Lng = c.Int(nullable: false),
                        Lat = c.Int(nullable: false),
                        Speed = c.Int(nullable: false),
                        TrackerTime = c.Int(nullable: false),
                        ServerTime = c.Int(nullable: false),
                        Place = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buses", t => t.BusId, cascadeDelete: true)
                .Index(t => t.BusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Markers", "BusId", "dbo.Buses");
            DropIndex("dbo.Markers", new[] { "BusId" });
            DropTable("dbo.Markers");
            DropTable("dbo.Buses");
        }
    }
}
