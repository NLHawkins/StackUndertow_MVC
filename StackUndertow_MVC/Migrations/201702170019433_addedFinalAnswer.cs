namespace StackUndertow_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedFinalAnswer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "FinalAnswer", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "FinalAnswer");
        }
    }
}
