namespace demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegisterDateToUser1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "RegistrationDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "RegistrationDate", c => c.DateTime(nullable: false));
        }
    }
}
