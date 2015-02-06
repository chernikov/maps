namespace maps.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BicycleDirectionLine",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BicycleDirectionID = c.Int(nullable: false),
                        BicycleLineID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BicycleLine", t => t.BicycleLineID, cascadeDelete: true)
                .ForeignKey("dbo.BycicleDirection", t => t.BicycleDirectionID, cascadeDelete: true)
                .Index(t => t.BicycleDirectionID)
                .Index(t => t.BicycleLineID);
            
            CreateTable(
                "dbo.BicycleLine",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CityID = c.Int(nullable: false),
                        Start = c.String(nullable: false, maxLength: 150),
                        End = c.String(nullable: false, maxLength: 150),
                        StartLat = c.Double(nullable: false),
                        StartLng = c.Double(nullable: false),
                        EndLat = c.Double(nullable: false),
                        EndLng = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.City", t => t.CityID)
                .Index(t => t.CityID);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        CenterLat = c.Double(nullable: false),
                        CenterLng = c.Double(nullable: false),
                        Zoom = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BicycleParking",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        CityID = c.Int(nullable: false),
                        Position = c.String(nullable: false, maxLength: 100),
                        PhotoUrl = c.String(maxLength: 150),
                        Exist = c.Boolean(nullable: false),
                        Type = c.Int(nullable: false),
                        Lock = c.Boolean(nullable: false),
                        Camera = c.Boolean(nullable: false),
                        Rent = c.Boolean(nullable: false),
                        Quality = c.Int(nullable: false),
                        Capacity = c.Int(nullable: false),
                        VotesCount = c.Int(nullable: false),
                        Description = c.String(),
                        AddedDate = c.DateTime(nullable: false),
                        VerifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(),
                        Address = c.String(),
                        CenterDistance = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.City", t => t.CityID)
                .Index(t => t.UserID)
                .Index(t => t.CityID);
            
            CreateTable(
                "dbo.BicycleParkingVote",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        BicycleParkingID = c.Int(nullable: false),
                        Mark = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BicycleParking", t => t.BicycleParkingID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.BicycleParkingID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CityID = c.Int(nullable: false),
                        Login = c.String(nullable: false, maxLength: 150),
                        Email = c.String(nullable: false, maxLength: 150),
                        Password = c.String(nullable: false, maxLength: 50),
                        AddedDate = c.DateTime(nullable: false),
                        LastVisitDate = c.DateTime(nullable: false),
                        AvatarPath = c.String(maxLength: 150),
                        FirstName = c.String(maxLength: 500),
                        LastName = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.City", t => t.CityID)
                .Index(t => t.CityID);
            
            CreateTable(
                "dbo.BycicleDirection",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CityID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Waypoints = c.String(nullable: false, unicode: false),
                        PolyLine = c.String(nullable: false, unicode: false),
                        Length = c.Double(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.City", t => t.CityID)
                .Index(t => t.CityID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        ParentID = c.Int(),
                        AddedDate = c.DateTime(nullable: false),
                        Text = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Comment", t => t.ParentID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.UtilityIssueComment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UtilityIssueID = c.Int(nullable: false),
                        CommentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Comment", t => t.CommentID, cascadeDelete: true)
                .ForeignKey("dbo.UtilityIssue", t => t.UtilityIssueID, cascadeDelete: true)
                .Index(t => t.UtilityIssueID)
                .Index(t => t.CommentID);
            
            CreateTable(
                "dbo.UtilityIssue",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        CityID = c.Int(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        AcceptedDate = c.DateTime(),
                        ResolvedDate = c.DateTime(),
                        ClosedDate = c.DateTime(),
                        UtilityDepartmentID = c.Int(),
                        MainUtilityIssueID = c.Int(),
                        Lat = c.Double(nullable: false),
                        Lng = c.Double(nullable: false),
                        Address = c.String(),
                        Description = c.String(),
                        Solution = c.String(),
                        Status = c.Int(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UtilityDepartment", t => t.UtilityDepartmentID)
                .ForeignKey("dbo.UtilityIssue", t => t.MainUtilityIssueID)
                .ForeignKey("dbo.User", t => t.UserID)
                .ForeignKey("dbo.City", t => t.CityID)
                .Index(t => t.UserID)
                .Index(t => t.CityID)
                .Index(t => t.UtilityDepartmentID)
                .Index(t => t.MainUtilityIssueID);
            
            CreateTable(
                "dbo.UtilityDepartment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CityID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        Phone = c.String(maxLength: 100),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.City", t => t.CityID)
                .Index(t => t.CityID);
            
            CreateTable(
                "dbo.UtilityIssueHistory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UtilityIssueID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        AcceptedDate = c.DateTime(),
                        ResolvedDate = c.DateTime(),
                        ClosedDate = c.DateTime(),
                        UtilityDepartmentID = c.Int(),
                        MainUtilityIssueID = c.Int(),
                        Lat = c.Double(nullable: false),
                        Lng = c.Double(nullable: false),
                        Address = c.String(),
                        Description = c.String(),
                        Solution = c.String(),
                        Status = c.Int(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UtilityDepartment", t => t.UtilityDepartmentID)
                .ForeignKey("dbo.UtilityIssue", t => t.UtilityIssueID, cascadeDelete: true)
                .ForeignKey("dbo.UtilityIssueHistory", t => t.MainUtilityIssueID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.UtilityIssueID)
                .Index(t => t.UserID)
                .Index(t => t.UtilityDepartmentID)
                .Index(t => t.MainUtilityIssueID);
            
            CreateTable(
                "dbo.UtilityIssueTag",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UtilityIssueID = c.Int(nullable: false),
                        UtilityTagID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UtilityIssue", t => t.UtilityIssueID, cascadeDelete: true)
                .ForeignKey("dbo.UtilityTag", t => t.UtilityTagID, cascadeDelete: true)
                .Index(t => t.UtilityIssueID)
                .Index(t => t.UtilityTagID);
            
            CreateTable(
                "dbo.UtilityTag",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UtilityPhoto",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UtilityIssueID = c.Int(),
                        UserID = c.Int(),
                        Image = c.String(nullable: false, maxLength: 150),
                        AddedDate = c.DateTime(nullable: false),
                        State = c.Int(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserID)
                .ForeignKey("dbo.UtilityIssue", t => t.UtilityIssueID)
                .Index(t => t.UtilityIssueID)
                .Index(t => t.UserID);
            
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Goal", t => t.GoalID, cascadeDelete: true)
                .Index(t => t.GoalID);
            
            CreateTable(
                "dbo.Social",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Identified = c.String(nullable: false),
                        Provider = c.Int(nullable: false),
                        UserInfo = c.String(),
                        JsonResource = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Role", t => t.RoleID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.RoleID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.InstagramPhoto",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GlobalId = c.String(nullable: false, maxLength: 500),
                        CityID = c.Int(nullable: false),
                        Lat = c.Double(nullable: false),
                        Lng = c.Double(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        PhotoUrl = c.String(nullable: false, maxLength: 200),
                        Caption = c.String(),
                        Tags = c.String(),
                        UserGlobalId = c.Long(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 200),
                        UserFullName = c.String(nullable: false, maxLength: 200),
                        AddedDate = c.DateTime(nullable: false),
                        Link = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.City", t => t.CityID)
                .Index(t => t.CityID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UtilityIssue", "CityID", "dbo.City");
            DropForeignKey("dbo.UtilityDepartment", "CityID", "dbo.City");
            DropForeignKey("dbo.User", "CityID", "dbo.City");
            DropForeignKey("dbo.InstagramPhoto", "CityID", "dbo.City");
            DropForeignKey("dbo.BycicleDirection", "CityID", "dbo.City");
            DropForeignKey("dbo.BicycleParking", "CityID", "dbo.City");
            DropForeignKey("dbo.UtilityIssue", "UserID", "dbo.User");
            DropForeignKey("dbo.UtilityIssueHistory", "UserID", "dbo.User");
            DropForeignKey("dbo.UserRole", "UserID", "dbo.User");
            DropForeignKey("dbo.UserRole", "RoleID", "dbo.Role");
            DropForeignKey("dbo.Social", "UserID", "dbo.User");
            DropForeignKey("dbo.Goal", "UserID", "dbo.User");
            DropForeignKey("dbo.GoalCell", "GoalID", "dbo.Goal");
            DropForeignKey("dbo.Comment", "UserID", "dbo.User");
            DropForeignKey("dbo.UtilityPhoto", "UtilityIssueID", "dbo.UtilityIssue");
            DropForeignKey("dbo.UtilityPhoto", "UserID", "dbo.User");
            DropForeignKey("dbo.UtilityIssueTag", "UtilityTagID", "dbo.UtilityTag");
            DropForeignKey("dbo.UtilityIssueTag", "UtilityIssueID", "dbo.UtilityIssue");
            DropForeignKey("dbo.UtilityIssue", "MainUtilityIssueID", "dbo.UtilityIssue");
            DropForeignKey("dbo.UtilityIssueComment", "UtilityIssueID", "dbo.UtilityIssue");
            DropForeignKey("dbo.UtilityIssue", "UtilityDepartmentID", "dbo.UtilityDepartment");
            DropForeignKey("dbo.UtilityIssueHistory", "MainUtilityIssueID", "dbo.UtilityIssueHistory");
            DropForeignKey("dbo.UtilityIssueHistory", "UtilityIssueID", "dbo.UtilityIssue");
            DropForeignKey("dbo.UtilityIssueHistory", "UtilityDepartmentID", "dbo.UtilityDepartment");
            DropForeignKey("dbo.UtilityIssueComment", "CommentID", "dbo.Comment");
            DropForeignKey("dbo.Comment", "ParentID", "dbo.Comment");
            DropForeignKey("dbo.BycicleDirection", "UserID", "dbo.User");
            DropForeignKey("dbo.BicycleDirectionLine", "BicycleDirectionID", "dbo.BycicleDirection");
            DropForeignKey("dbo.BicycleParkingVote", "UserID", "dbo.User");
            DropForeignKey("dbo.BicycleParking", "UserID", "dbo.User");
            DropForeignKey("dbo.BicycleParkingVote", "BicycleParkingID", "dbo.BicycleParking");
            DropForeignKey("dbo.BicycleLine", "CityID", "dbo.City");
            DropForeignKey("dbo.BicycleDirectionLine", "BicycleLineID", "dbo.BicycleLine");
            DropIndex("dbo.InstagramPhoto", new[] { "CityID" });
            DropIndex("dbo.UserRole", new[] { "UserID" });
            DropIndex("dbo.UserRole", new[] { "RoleID" });
            DropIndex("dbo.Social", new[] { "UserID" });
            DropIndex("dbo.GoalCell", new[] { "GoalID" });
            DropIndex("dbo.Goal", new[] { "UserID" });
            DropIndex("dbo.UtilityPhoto", new[] { "UserID" });
            DropIndex("dbo.UtilityPhoto", new[] { "UtilityIssueID" });
            DropIndex("dbo.UtilityIssueTag", new[] { "UtilityTagID" });
            DropIndex("dbo.UtilityIssueTag", new[] { "UtilityIssueID" });
            DropIndex("dbo.UtilityIssueHistory", new[] { "MainUtilityIssueID" });
            DropIndex("dbo.UtilityIssueHistory", new[] { "UtilityDepartmentID" });
            DropIndex("dbo.UtilityIssueHistory", new[] { "UserID" });
            DropIndex("dbo.UtilityIssueHistory", new[] { "UtilityIssueID" });
            DropIndex("dbo.UtilityDepartment", new[] { "CityID" });
            DropIndex("dbo.UtilityIssue", new[] { "MainUtilityIssueID" });
            DropIndex("dbo.UtilityIssue", new[] { "UtilityDepartmentID" });
            DropIndex("dbo.UtilityIssue", new[] { "CityID" });
            DropIndex("dbo.UtilityIssue", new[] { "UserID" });
            DropIndex("dbo.UtilityIssueComment", new[] { "CommentID" });
            DropIndex("dbo.UtilityIssueComment", new[] { "UtilityIssueID" });
            DropIndex("dbo.Comment", new[] { "ParentID" });
            DropIndex("dbo.Comment", new[] { "UserID" });
            DropIndex("dbo.BycicleDirection", new[] { "UserID" });
            DropIndex("dbo.BycicleDirection", new[] { "CityID" });
            DropIndex("dbo.User", new[] { "CityID" });
            DropIndex("dbo.BicycleParkingVote", new[] { "BicycleParkingID" });
            DropIndex("dbo.BicycleParkingVote", new[] { "UserID" });
            DropIndex("dbo.BicycleParking", new[] { "CityID" });
            DropIndex("dbo.BicycleParking", new[] { "UserID" });
            DropIndex("dbo.BicycleLine", new[] { "CityID" });
            DropIndex("dbo.BicycleDirectionLine", new[] { "BicycleLineID" });
            DropIndex("dbo.BicycleDirectionLine", new[] { "BicycleDirectionID" });
            DropTable("dbo.InstagramPhoto");
            DropTable("dbo.Role");
            DropTable("dbo.UserRole");
            DropTable("dbo.Social");
            DropTable("dbo.GoalCell");
            DropTable("dbo.Goal");
            DropTable("dbo.UtilityPhoto");
            DropTable("dbo.UtilityTag");
            DropTable("dbo.UtilityIssueTag");
            DropTable("dbo.UtilityIssueHistory");
            DropTable("dbo.UtilityDepartment");
            DropTable("dbo.UtilityIssue");
            DropTable("dbo.UtilityIssueComment");
            DropTable("dbo.Comment");
            DropTable("dbo.BycicleDirection");
            DropTable("dbo.User");
            DropTable("dbo.BicycleParkingVote");
            DropTable("dbo.BicycleParking");
            DropTable("dbo.City");
            DropTable("dbo.BicycleLine");
            DropTable("dbo.BicycleDirectionLine");
        }
    }
}
