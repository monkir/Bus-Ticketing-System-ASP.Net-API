namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ticket_seat_no : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tickets", "seat_no", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tickets", "seat_no");
        }
    }
}
