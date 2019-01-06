namespace demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIsActiveFromMessage : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Messages", "IsMessageActive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "IsMessageActive", c => c.Boolean(nullable: false));
        }
    }
}
