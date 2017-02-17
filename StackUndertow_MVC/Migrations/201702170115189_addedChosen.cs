namespace StackUndertow_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedChosen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "Chosen", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Answers", "Chosen");
        }
    }
}
