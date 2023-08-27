namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class discountCupon_changes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.discountCupons", "cupon", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.discountCupons", "cupon");
        }
    }
}
