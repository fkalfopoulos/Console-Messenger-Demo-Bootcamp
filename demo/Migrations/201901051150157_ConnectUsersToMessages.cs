namespace demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConnectUsersToMessages : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Messages", new[] { "User_Id" });
            RenameColumn(table: "dbo.Messages", name: "User_Id", newName: "RecieverId");
            AddColumn("dbo.Messages", "SenderId", c => c.Int(nullable: false));
            AlterColumn("dbo.Messages", "RecieverId", c => c.Int(nullable: false));
            CreateIndex("dbo.Messages", "SenderId");
            CreateIndex("dbo.Messages", "RecieverId");
            AddForeignKey("dbo.Messages", "SenderId", "dbo.Users", "Id");
            DropColumn("dbo.Messages", "Sender");
            DropColumn("dbo.Messages", "Receiver");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "Receiver", c => c.String());
            AddColumn("dbo.Messages", "Sender", c => c.String());
            DropForeignKey("dbo.Messages", "SenderId", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "RecieverId" });
            DropIndex("dbo.Messages", new[] { "SenderId" });
            AlterColumn("dbo.Messages", "RecieverId", c => c.Int());
            DropColumn("dbo.Messages", "SenderId");
            RenameColumn(table: "dbo.Messages", name: "RecieverId", newName: "User_Id");
            CreateIndex("dbo.Messages", "User_Id");
        }
    }
}
