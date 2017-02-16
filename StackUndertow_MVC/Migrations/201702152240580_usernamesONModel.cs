namespace StackUndertow_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usernamesONModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "AOwnerName", c => c.String());
            AddColumn("dbo.Questions", "QOwnerName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "QOwnerName");
            DropColumn("dbo.Answers", "AOwnerName");
        }
    }
}
