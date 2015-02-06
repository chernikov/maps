namespace maps.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GoalsOf : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GoalCell", "GoalID", "dbo.Goal");
            DropForeignKey("dbo.Goal", "UserID", "dbo.User");
            DropIndex("dbo.Goal", new[] { "UserID" });
            DropIndex("dbo.GoalCell", new[] { "GoalID" });
            DropTable("dbo.Goal");
            DropTable("dbo.GoalCell");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GoalCell",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GoalID = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                        AddedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Goal",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        Url = c.String(nullable: false, maxLength: 140),
                        Text = c.String(nullable: false, maxLength: 140),
                        Count = c.Int(nullable: false),
                        Progress = c.Int(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        IsReady = c.Boolean(nullable: false),
                        ColumnsCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.GoalCell", "GoalID");
            CreateIndex("dbo.Goal", "UserID");
            AddForeignKey("dbo.Goal", "UserID", "dbo.User", "ID", cascadeDelete: true);
            AddForeignKey("dbo.GoalCell", "GoalID", "dbo.Goal", "ID", cascadeDelete: true);
        }
    }
}
