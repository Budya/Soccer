namespace SoccerId.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SoccerLeagues.AgreedPlayersLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        _Team_Id = c.Int(),
                        _TeamEvent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SoccerLeagues.Teams", t => t._Team_Id)
                .ForeignKey("SoccerLeagues.TeamEvents", t => t._TeamEvent_Id)
                .Index(t => t._Team_Id)
                .Index(t => t._TeamEvent_Id);
            
            CreateTable(
                "SoccerLeagues.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamName = c.String(maxLength: 50),
                        CreationTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        _CurrentLeague_Id = c.Int(),
                        _Logo_Id = c.Int(),
                        _TeamManager_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SoccerLeagues.Leagues", t => t._CurrentLeague_Id)
                .ForeignKey("SoccerLeagues.TeamLogos", t => t._Logo_Id)
                .ForeignKey("SoccerLeagues.Users", t => t._TeamManager_Id)
                .Index(t => t._CurrentLeague_Id)
                .Index(t => t._Logo_Id)
                .Index(t => t._TeamManager_Id);
            
            CreateTable(
                "SoccerLeagues.Leagues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        _Logo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SoccerLeagues.LeagueLogos", t => t._Logo_Id)
                .Index(t => t._Logo_Id);
            
            CreateTable(
                "SoccerLeagues.LeagueLogos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SmallImage = c.Binary(),
                        Image = c.Binary(),
                        PhotoName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SoccerLeagues.LeaguePhoto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.Binary(),
                        PhotoName = c.String(maxLength: 50),
                        League_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SoccerLeagues.Leagues", t => t.League_Id)
                .Index(t => t.League_Id);
            
            CreateTable(
                "SoccerLeagues.TeamLogos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SmallImage = c.Binary(),
                        Image = c.Binary(),
                        PhotoName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SoccerLeagues.TeamPhoto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.Binary(),
                        PhotoName = c.String(maxLength: 50),
                        Team_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SoccerLeagues.Teams", t => t.Team_Id)
                .Index(t => t.Team_Id);
            
            CreateTable(
                "SoccerLeagues.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Birthday = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Email = c.String(maxLength: 256),
                        PasswordHash = c.String(),
                        PhoneNumber = c.String(),
                        UserName = c.String(nullable: false, maxLength: 256),
                        _CurrentTeam_Id = c.Int(),
                        _MajorPhoto_Id = c.Int(),
                        Team_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SoccerLeagues.Teams", t => t._CurrentTeam_Id)
                .ForeignKey("SoccerLeagues.UserPhoto", t => t._MajorPhoto_Id)
                .ForeignKey("SoccerLeagues.Teams", t => t.Team_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t._CurrentTeam_Id)
                .Index(t => t._MajorPhoto_Id)
                .Index(t => t.Team_Id);
            
            CreateTable(
                "SoccerLeagues.Chats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Beginning = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        End = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        _Creator_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SoccerLeagues.Users", t => t._Creator_Id)
                .Index(t => t._Creator_Id);
            
            CreateTable(
                "SoccerLeagues.UserPhoto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.Binary(),
                        PhotoName = c.String(maxLength: 50),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SoccerLeagues.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "SoccerLeagues.PlayingPositions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PositionFirstName = c.String(maxLength: 50),
                        PositionLastName = c.String(maxLength: 50),
                        Abbreviation = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SoccerLeagues.PrivateMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(maxLength: 4000),
                        SendDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ReadDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Sender_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SoccerLeagues.Users", t => t.Sender_Id)
                .Index(t => t.Sender_Id);
            
            CreateTable(
                "SoccerLeagues.UsersArchiveTeams",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        ArchiveTeam_Id = c.Int(nullable: false),
                        Beginning = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        End = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        _ArchiveTeam_Id = c.Int(),
                        _User_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.User_Id, t.ArchiveTeam_Id })
                .ForeignKey("SoccerLeagues.ArchiveTeams", t => t._ArchiveTeam_Id)
                .ForeignKey("SoccerLeagues.Users", t => t._User_Id)
                .Index(t => t._ArchiveTeam_Id)
                .Index(t => t._User_Id);
            
            CreateTable(
                "SoccerLeagues.ArchiveTeams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamName = c.String(maxLength: 50),
                        CreationTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        _Logo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SoccerLeagues.TeamLogos", t => t._Logo_Id)
                .Index(t => t._Logo_Id);
            
            CreateTable(
                "SoccerLeagues.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SoccerLeagues.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "SoccerLeagues.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("SoccerLeagues.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "SoccerLeagues.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("SoccerLeagues.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("SoccerLeagues.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "SoccerLeagues.TeamEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        _Place_Id = c.Int(),
                        _Team1_Id = c.Int(),
                        _Team2_Id = c.Int(),
                        _Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SoccerLeagues.EventPlaces", t => t._Place_Id)
                .ForeignKey("SoccerLeagues.Teams", t => t._Team1_Id)
                .ForeignKey("SoccerLeagues.Teams", t => t._Team2_Id)
                .ForeignKey("SoccerLeagues.EventTypes", t => t._Type_Id)
                .Index(t => t._Place_Id)
                .Index(t => t._Team1_Id)
                .Index(t => t._Team2_Id)
                .Index(t => t._Type_Id);
            
            CreateTable(
                "SoccerLeagues.EventPlaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Address = c.String(maxLength: 200),
                        Map = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SoccerLeagues.EventTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SoccerLeagues.ChatMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(maxLength: 4000),
                        SendDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ReadDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        _Chat_Id = c.Int(),
                        Sender_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SoccerLeagues.Chats", t => t._Chat_Id)
                .ForeignKey("SoccerLeagues.Users", t => t.Sender_Id)
                .Index(t => t._Chat_Id)
                .Index(t => t.Sender_Id);
            
            CreateTable(
                "SoccerLeagues.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "SoccerLeagues.VisitLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Beginning = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        End = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        _User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SoccerLeagues.Users", t => t._User_Id)
                .Index(t => t._User_Id);
            
            CreateTable(
                "SoccerLeagues.UserAgreedPlayersLists",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        AgreedPlayersList_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.AgreedPlayersList_Id })
                .ForeignKey("SoccerLeagues.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("SoccerLeagues.AgreedPlayersLists", t => t.AgreedPlayersList_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.AgreedPlayersList_Id);
            
            CreateTable(
                "SoccerLeagues.UsersChats",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Chat_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Chat_Id })
                .ForeignKey("SoccerLeagues.Chats", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("SoccerLeagues.Users", t => t.Chat_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Chat_Id);
            
            CreateTable(
                "SoccerLeagues.PlayingPositionUsers",
                c => new
                    {
                        PlayingPosition_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayingPosition_Id, t.User_Id })
                .ForeignKey("SoccerLeagues.PlayingPositions", t => t.PlayingPosition_Id, cascadeDelete: true)
                .ForeignKey("SoccerLeagues.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.PlayingPosition_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "SoccerLeagues.UsersPrivateMessages",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        PrivateMessage_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.PrivateMessage_Id })
                .ForeignKey("SoccerLeagues.PrivateMessages", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("SoccerLeagues.Users", t => t.PrivateMessage_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.PrivateMessage_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("SoccerLeagues.VisitLogs", "_User_Id", "SoccerLeagues.Users");
            DropForeignKey("SoccerLeagues.UserRoles", "RoleId", "SoccerLeagues.Roles");
            DropForeignKey("SoccerLeagues.ChatMessages", "Sender_Id", "SoccerLeagues.Users");
            DropForeignKey("SoccerLeagues.ChatMessages", "_Chat_Id", "SoccerLeagues.Chats");
            DropForeignKey("SoccerLeagues.AgreedPlayersLists", "_TeamEvent_Id", "SoccerLeagues.TeamEvents");
            DropForeignKey("SoccerLeagues.TeamEvents", "_Type_Id", "SoccerLeagues.EventTypes");
            DropForeignKey("SoccerLeagues.TeamEvents", "_Team2_Id", "SoccerLeagues.Teams");
            DropForeignKey("SoccerLeagues.TeamEvents", "_Team1_Id", "SoccerLeagues.Teams");
            DropForeignKey("SoccerLeagues.TeamEvents", "_Place_Id", "SoccerLeagues.EventPlaces");
            DropForeignKey("SoccerLeagues.AgreedPlayersLists", "_Team_Id", "SoccerLeagues.Teams");
            DropForeignKey("SoccerLeagues.Teams", "_TeamManager_Id", "SoccerLeagues.Users");
            DropForeignKey("SoccerLeagues.Users", "Team_Id", "SoccerLeagues.Teams");
            DropForeignKey("SoccerLeagues.UserRoles", "UserId", "SoccerLeagues.Users");
            DropForeignKey("SoccerLeagues.UserLogins", "UserId", "SoccerLeagues.Users");
            DropForeignKey("SoccerLeagues.UserClaims", "UserId", "SoccerLeagues.Users");
            DropForeignKey("SoccerLeagues.UsersArchiveTeams", "_User_Id", "SoccerLeagues.Users");
            DropForeignKey("SoccerLeagues.UsersArchiveTeams", "_ArchiveTeam_Id", "SoccerLeagues.ArchiveTeams");
            DropForeignKey("SoccerLeagues.ArchiveTeams", "_Logo_Id", "SoccerLeagues.TeamLogos");
            DropForeignKey("SoccerLeagues.PrivateMessages", "Sender_Id", "SoccerLeagues.Users");
            DropForeignKey("SoccerLeagues.UsersPrivateMessages", "PrivateMessage_Id", "SoccerLeagues.Users");
            DropForeignKey("SoccerLeagues.UsersPrivateMessages", "User_Id", "SoccerLeagues.PrivateMessages");
            DropForeignKey("SoccerLeagues.PlayingPositionUsers", "User_Id", "SoccerLeagues.Users");
            DropForeignKey("SoccerLeagues.PlayingPositionUsers", "PlayingPosition_Id", "SoccerLeagues.PlayingPositions");
            DropForeignKey("SoccerLeagues.UserPhoto", "User_Id", "SoccerLeagues.Users");
            DropForeignKey("SoccerLeagues.Users", "_MajorPhoto_Id", "SoccerLeagues.UserPhoto");
            DropForeignKey("SoccerLeagues.Users", "_CurrentTeam_Id", "SoccerLeagues.Teams");
            DropForeignKey("SoccerLeagues.UsersChats", "Chat_Id", "SoccerLeagues.Users");
            DropForeignKey("SoccerLeagues.UsersChats", "User_Id", "SoccerLeagues.Chats");
            DropForeignKey("SoccerLeagues.Chats", "_Creator_Id", "SoccerLeagues.Users");
            DropForeignKey("SoccerLeagues.UserAgreedPlayersLists", "AgreedPlayersList_Id", "SoccerLeagues.AgreedPlayersLists");
            DropForeignKey("SoccerLeagues.UserAgreedPlayersLists", "User_Id", "SoccerLeagues.Users");
            DropForeignKey("SoccerLeagues.TeamPhoto", "Team_Id", "SoccerLeagues.Teams");
            DropForeignKey("SoccerLeagues.Teams", "_Logo_Id", "SoccerLeagues.TeamLogos");
            DropForeignKey("SoccerLeagues.Teams", "_CurrentLeague_Id", "SoccerLeagues.Leagues");
            DropForeignKey("SoccerLeagues.LeaguePhoto", "League_Id", "SoccerLeagues.Leagues");
            DropForeignKey("SoccerLeagues.Leagues", "_Logo_Id", "SoccerLeagues.LeagueLogos");
            DropIndex("SoccerLeagues.UsersPrivateMessages", new[] { "PrivateMessage_Id" });
            DropIndex("SoccerLeagues.UsersPrivateMessages", new[] { "User_Id" });
            DropIndex("SoccerLeagues.PlayingPositionUsers", new[] { "User_Id" });
            DropIndex("SoccerLeagues.PlayingPositionUsers", new[] { "PlayingPosition_Id" });
            DropIndex("SoccerLeagues.UsersChats", new[] { "Chat_Id" });
            DropIndex("SoccerLeagues.UsersChats", new[] { "User_Id" });
            DropIndex("SoccerLeagues.UserAgreedPlayersLists", new[] { "AgreedPlayersList_Id" });
            DropIndex("SoccerLeagues.UserAgreedPlayersLists", new[] { "User_Id" });
            DropIndex("SoccerLeagues.VisitLogs", new[] { "_User_Id" });
            DropIndex("SoccerLeagues.Roles", "RoleNameIndex");
            DropIndex("SoccerLeagues.ChatMessages", new[] { "Sender_Id" });
            DropIndex("SoccerLeagues.ChatMessages", new[] { "_Chat_Id" });
            DropIndex("SoccerLeagues.TeamEvents", new[] { "_Type_Id" });
            DropIndex("SoccerLeagues.TeamEvents", new[] { "_Team2_Id" });
            DropIndex("SoccerLeagues.TeamEvents", new[] { "_Team1_Id" });
            DropIndex("SoccerLeagues.TeamEvents", new[] { "_Place_Id" });
            DropIndex("SoccerLeagues.UserRoles", new[] { "RoleId" });
            DropIndex("SoccerLeagues.UserRoles", new[] { "UserId" });
            DropIndex("SoccerLeagues.UserLogins", new[] { "UserId" });
            DropIndex("SoccerLeagues.UserClaims", new[] { "UserId" });
            DropIndex("SoccerLeagues.ArchiveTeams", new[] { "_Logo_Id" });
            DropIndex("SoccerLeagues.UsersArchiveTeams", new[] { "_User_Id" });
            DropIndex("SoccerLeagues.UsersArchiveTeams", new[] { "_ArchiveTeam_Id" });
            DropIndex("SoccerLeagues.PrivateMessages", new[] { "Sender_Id" });
            DropIndex("SoccerLeagues.UserPhoto", new[] { "User_Id" });
            DropIndex("SoccerLeagues.Chats", new[] { "_Creator_Id" });
            DropIndex("SoccerLeagues.Users", new[] { "Team_Id" });
            DropIndex("SoccerLeagues.Users", new[] { "_MajorPhoto_Id" });
            DropIndex("SoccerLeagues.Users", new[] { "_CurrentTeam_Id" });
            DropIndex("SoccerLeagues.Users", "UserNameIndex");
            DropIndex("SoccerLeagues.TeamPhoto", new[] { "Team_Id" });
            DropIndex("SoccerLeagues.LeaguePhoto", new[] { "League_Id" });
            DropIndex("SoccerLeagues.Leagues", new[] { "_Logo_Id" });
            DropIndex("SoccerLeagues.Teams", new[] { "_TeamManager_Id" });
            DropIndex("SoccerLeagues.Teams", new[] { "_Logo_Id" });
            DropIndex("SoccerLeagues.Teams", new[] { "_CurrentLeague_Id" });
            DropIndex("SoccerLeagues.AgreedPlayersLists", new[] { "_TeamEvent_Id" });
            DropIndex("SoccerLeagues.AgreedPlayersLists", new[] { "_Team_Id" });
            DropTable("SoccerLeagues.UsersPrivateMessages");
            DropTable("SoccerLeagues.PlayingPositionUsers");
            DropTable("SoccerLeagues.UsersChats");
            DropTable("SoccerLeagues.UserAgreedPlayersLists");
            DropTable("SoccerLeagues.VisitLogs");
            DropTable("SoccerLeagues.Roles");
            DropTable("SoccerLeagues.ChatMessages");
            DropTable("SoccerLeagues.EventTypes");
            DropTable("SoccerLeagues.EventPlaces");
            DropTable("SoccerLeagues.TeamEvents");
            DropTable("SoccerLeagues.UserRoles");
            DropTable("SoccerLeagues.UserLogins");
            DropTable("SoccerLeagues.UserClaims");
            DropTable("SoccerLeagues.ArchiveTeams");
            DropTable("SoccerLeagues.UsersArchiveTeams");
            DropTable("SoccerLeagues.PrivateMessages");
            DropTable("SoccerLeagues.PlayingPositions");
            DropTable("SoccerLeagues.UserPhoto");
            DropTable("SoccerLeagues.Chats");
            DropTable("SoccerLeagues.Users");
            DropTable("SoccerLeagues.TeamPhoto");
            DropTable("SoccerLeagues.TeamLogos");
            DropTable("SoccerLeagues.LeaguePhoto");
            DropTable("SoccerLeagues.LeagueLogos");
            DropTable("SoccerLeagues.Leagues");
            DropTable("SoccerLeagues.Teams");
            DropTable("SoccerLeagues.AgreedPlayersLists");
        }
    }
}
