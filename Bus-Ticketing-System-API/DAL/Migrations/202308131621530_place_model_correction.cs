namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class place_model_correction : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.places", "employee_id", "dbo.employees");
            DropIndex("dbo.places", new[] { "employee_id" });
            RenameColumn(table: "dbo.places", name: "employee_id", newName: "emp_id");
            AlterColumn("dbo.places", "emp_id", c => c.Int(nullable: false));
            CreateIndex("dbo.places", "emp_id");
            AddForeignKey("dbo.places", "emp_id", "dbo.employees", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.places", "emp_id", "dbo.employees");
            DropIndex("dbo.places", new[] { "emp_id" });
            AlterColumn("dbo.places", "emp_id", c => c.Int());
            RenameColumn(table: "dbo.places", name: "emp_id", newName: "employee_id");
            CreateIndex("dbo.places", "employee_id");
            AddForeignKey("dbo.places", "employee_id", "dbo.employees", "id");
        }
    }
}
