namespace StackUndertow_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedAScoreUScoreVoteType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "AScore", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "UScore", c => c.Int(nullable: false));
            AddColumn("dbo.UpVotes", "VoteType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UpVotes", "VoteType");
            DropColumn("dbo.AspNetUsers", "UScore");
            DropColumn("dbo.Answers", "AScore");
        }
    }
}
