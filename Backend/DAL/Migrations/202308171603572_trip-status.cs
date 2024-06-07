namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tripstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.trips", "status", c => c.String());
            AddColumn("dbo.trips", "emp_id", c => c.Int());
            CreateIndex("dbo.trips", "emp_id");
            AddForeignKey("dbo.trips", "emp_id", "dbo.employees", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.trips", "emp_id", "dbo.employees");
            DropIndex("dbo.trips", new[] { "emp_id" });
            DropColumn("dbo.trips", "emp_id");
            DropColumn("dbo.trips", "status");
        }
    }
}
