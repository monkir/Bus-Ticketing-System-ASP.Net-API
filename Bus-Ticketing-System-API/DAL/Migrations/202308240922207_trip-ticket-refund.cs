namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tripticketrefund : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.transactions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        details = c.String(),
                        amount = c.Int(nullable: false),
                        time = c.DateTime(nullable: false),
                        userID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.users", t => t.userID, cascadeDelete: true)
                .Index(t => t.userID);
            
            AddColumn("dbo.tickets", "ammount", c => c.Int(nullable: false));
            AddColumn("dbo.trips", "startTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.trips", "endTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.transactions", "userID", "dbo.users");
            DropIndex("dbo.transactions", new[] { "userID" });
            DropColumn("dbo.trips", "endTime");
            DropColumn("dbo.trips", "startTime");
            DropColumn("dbo.tickets", "ammount");
            DropTable("dbo.transactions");
        }
    }
}
