namespace demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegisterDateToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "RegistrationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "RegistrationDate");
        }
    }
}
