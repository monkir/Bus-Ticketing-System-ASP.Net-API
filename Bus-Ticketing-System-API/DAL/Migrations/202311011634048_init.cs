namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.admins",
                c => new
                    {
                        id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.users", t => t.id)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.discountCupons",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        cupon = c.String(),
                        percentage = c.Single(nullable: false),
                        maxDiscount = c.Int(nullable: false),
                        admin_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.admins", t => t.admin_id)
                .Index(t => t.admin_id);
            
            CreateTable(
                "dbo.tickets",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        seat_no = c.String(),
                        ammount = c.Int(nullable: false),
                        status = c.String(),
                        dc_id = c.Int(),
                        trip_id = c.Int(),
                        cust_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.customers", t => t.cust_id)
                .ForeignKey("dbo.discountCupons", t => t.dc_id)
                .ForeignKey("dbo.trips", t => t.trip_id)
                .Index(t => t.dc_id)
                .Index(t => t.trip_id)
                .Index(t => t.cust_id);
            
            CreateTable(
                "dbo.customers",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.users", t => t.id)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        password = c.String(),
                        userRole = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.busProviders",
                c => new
                    {
                        id = c.Int(nullable: false),
                        company = c.String(),
                        emp_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.employees", t => t.emp_id)
                .ForeignKey("dbo.users", t => t.id)
                .Index(t => t.id)
                .Index(t => t.emp_id);
            
            CreateTable(
                "dbo.buses",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        brand = c.String(),
                        model = c.String(),
                        serialNo = c.String(),
                        category = c.String(),
                        totalSeat = c.Int(nullable: false),
                        bp_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.busProviders", t => t.bp_id)
                .Index(t => t.bp_id);
            
            CreateTable(
                "dbo.trips",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ticketPrice = c.Int(nullable: false),
                        status = c.String(),
                        startTime = c.DateTime(nullable: false),
                        endTime = c.DateTime(nullable: false),
                        emp_id = c.Int(),
                        depot_id = c.Int(),
                        dest_id = c.Int(),
                        bus_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.buses", t => t.bus_id)
                .ForeignKey("dbo.places", t => t.depot_id)
                .ForeignKey("dbo.places", t => t.dest_id)
                .ForeignKey("dbo.employees", t => t.emp_id)
                .Index(t => t.emp_id)
                .Index(t => t.depot_id)
                .Index(t => t.dest_id)
                .Index(t => t.bus_id);
            
            CreateTable(
                "dbo.places",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        emp_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.employees", t => t.emp_id)
                .Index(t => t.emp_id);
            
            CreateTable(
                "dbo.employees",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(),
                        salary = c.Int(nullable: false),
                        dob = c.DateTime(nullable: false),
                        admin_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.admins", t => t.admin_id)
                .ForeignKey("dbo.users", t => t.id)
                .Index(t => t.id)
                .Index(t => t.admin_id);
            
            CreateTable(
                "dbo.notices",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        description = c.String(),
                        emp_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.employees", t => t.emp_id)
                .Index(t => t.emp_id);
            
            CreateTable(
                "dbo.transactions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        details = c.String(),
                        amount = c.Int(nullable: false),
                        time = c.DateTime(nullable: false),
                        userID = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.users", t => t.userID)
                .Index(t => t.userID);
            
            CreateTable(
                "dbo.tokens",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        token_string = c.String(),
                        expireTime = c.DateTime(nullable: false),
                        userid = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.users", t => t.userid)
                .Index(t => t.userid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tokens", "userid", "dbo.users");
            DropForeignKey("dbo.admins", "id", "dbo.users");
            DropForeignKey("dbo.tickets", "trip_id", "dbo.trips");
            DropForeignKey("dbo.tickets", "dc_id", "dbo.discountCupons");
            DropForeignKey("dbo.tickets", "cust_id", "dbo.customers");
            DropForeignKey("dbo.customers", "id", "dbo.users");
            DropForeignKey("dbo.transactions", "userID", "dbo.users");
            DropForeignKey("dbo.busProviders", "id", "dbo.users");
            DropForeignKey("dbo.busProviders", "emp_id", "dbo.employees");
            DropForeignKey("dbo.trips", "emp_id", "dbo.employees");
            DropForeignKey("dbo.places", "emp_id", "dbo.employees");
            DropForeignKey("dbo.employees", "id", "dbo.users");
            DropForeignKey("dbo.notices", "emp_id", "dbo.employees");
            DropForeignKey("dbo.employees", "admin_id", "dbo.admins");
            DropForeignKey("dbo.trips", "dest_id", "dbo.places");
            DropForeignKey("dbo.trips", "depot_id", "dbo.places");
            DropForeignKey("dbo.trips", "bus_id", "dbo.buses");
            DropForeignKey("dbo.buses", "bp_id", "dbo.busProviders");
            DropForeignKey("dbo.discountCupons", "admin_id", "dbo.admins");
            DropIndex("dbo.tokens", new[] { "userid" });
            DropIndex("dbo.transactions", new[] { "userID" });
            DropIndex("dbo.notices", new[] { "emp_id" });
            DropIndex("dbo.employees", new[] { "admin_id" });
            DropIndex("dbo.employees", new[] { "id" });
            DropIndex("dbo.places", new[] { "emp_id" });
            DropIndex("dbo.trips", new[] { "bus_id" });
            DropIndex("dbo.trips", new[] { "dest_id" });
            DropIndex("dbo.trips", new[] { "depot_id" });
            DropIndex("dbo.trips", new[] { "emp_id" });
            DropIndex("dbo.buses", new[] { "bp_id" });
            DropIndex("dbo.busProviders", new[] { "emp_id" });
            DropIndex("dbo.busProviders", new[] { "id" });
            DropIndex("dbo.customers", new[] { "id" });
            DropIndex("dbo.tickets", new[] { "cust_id" });
            DropIndex("dbo.tickets", new[] { "trip_id" });
            DropIndex("dbo.tickets", new[] { "dc_id" });
            DropIndex("dbo.discountCupons", new[] { "admin_id" });
            DropIndex("dbo.admins", new[] { "id" });
            DropTable("dbo.tokens");
            DropTable("dbo.transactions");
            DropTable("dbo.notices");
            DropTable("dbo.employees");
            DropTable("dbo.places");
            DropTable("dbo.trips");
            DropTable("dbo.buses");
            DropTable("dbo.busProviders");
            DropTable("dbo.users");
            DropTable("dbo.customers");
            DropTable("dbo.tickets");
            DropTable("dbo.discountCupons");
            DropTable("dbo.admins");
        }
    }
}
