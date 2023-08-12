namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moreentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.discountCupons", "percentage", c => c.Single(nullable: false));
            AddColumn("dbo.discountCupons", "maxDiscount", c => c.Int(nullable: false));
            DropColumn("dbo.users", "userID");
            DropColumn("dbo.users", "userRoleID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.users", "userRoleID", c => c.Int(nullable: false));
            AddColumn("dbo.users", "userID", c => c.Int(nullable: false));
            DropColumn("dbo.discountCupons", "maxDiscount");
            DropColumn("dbo.discountCupons", "percentage");
        }
    }
}
