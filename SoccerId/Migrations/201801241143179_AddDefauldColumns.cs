namespace SoccerId.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDefauldColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("SoccerLeagues.Users", "EmailConfirmed", c => c.Boolean(nullable: false));
            AddColumn("SoccerLeagues.Users", "SecurityStamp", c => c.String());
            AddColumn("SoccerLeagues.Users", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("SoccerLeagues.Users", "TwoFactorEnabled", c => c.Boolean(nullable: false));
            AddColumn("SoccerLeagues.Users", "LockoutEndDateUtc", c => c.DateTime());
            AddColumn("SoccerLeagues.Users", "LockoutEnabled", c => c.Boolean(nullable: false));
            AddColumn("SoccerLeagues.Users", "AccessFailedCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("SoccerLeagues.Users", "AccessFailedCount");
            DropColumn("SoccerLeagues.Users", "LockoutEnabled");
            DropColumn("SoccerLeagues.Users", "LockoutEndDateUtc");
            DropColumn("SoccerLeagues.Users", "TwoFactorEnabled");
            DropColumn("SoccerLeagues.Users", "PhoneNumberConfirmed");
            DropColumn("SoccerLeagues.Users", "SecurityStamp");
            DropColumn("SoccerLeagues.Users", "EmailConfirmed");
        }
    }
}
