namespace demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newchangestomessages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "IsMessageShownToReciever", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "IsMessageShownToSender", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "IsMessageRead", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "IsMessageRead");
            DropColumn("dbo.Messages", "IsMessageShownToSender");
            DropColumn("dbo.Messages", "IsMessageShownToReciever");
        }
    }
}
