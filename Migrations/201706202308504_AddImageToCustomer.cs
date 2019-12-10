namespace CustomerManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageToCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Imageurl", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Imageurl");
        }
    }
}
