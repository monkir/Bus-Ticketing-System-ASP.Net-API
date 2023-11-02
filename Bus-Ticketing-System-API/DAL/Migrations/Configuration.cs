namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.EF.BTSContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.EF.BTSContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.


            // Admin
            context.user.AddOrUpdate(
                new EF.Models.user()
                {
                    id = 1,
                    username = "monkir",
                    password = "123",
                    userRole = "admin"
                });
            context.admin.AddOrUpdate(
                new EF.Models.admin()
                {
                    id = 1,
                });
            context.user.AddOrUpdate(
                new EF.Models.user()
                {
                    id = 2,
                    username = "forhad",
                    password = "123",
                    userRole = "admin"
                });
            context.admin.AddOrUpdate(
                new EF.Models.admin()
                {
                    id = 2
                });
            Random random = new Random();
            //cupon 
            for (int i = 0; i < 20; i++)
            {
                context.discountCupon.AddOrUpdate(
                    new EF.Models.discountCupon()
                    {
                        id = i + 1,
                        name = "cupon" + i,
                        cupon = "cp" + random.Next(3000, 9000),
                        percentage = random.Next(10, 30),
                        maxDiscount = random.Next(1, 10) * 100,
                        admin_id = random.Next(1, 3)
                    });
            }
            //employee
            for (int i = 3; i < 10; i++)
            {
                context.user.AddOrUpdate(
                    new EF.Models.user()
                    {
                        id = i,
                        username = "employee" + i,
                        password = "123",
                        userRole = "employee"
                    });
                context.employee.AddOrUpdate(
                    new EF.Models.employee()
                    {
                        id = i,
                        name = "Mr. Employee " + i,
                        dob = DateTime.Now.AddYears(random.Next(-30, -20)).AddMonths(random.Next(1, 13)).AddDays(random.Next(1, 32)),
                        salary = random.Next(15, 50) * 1000,
                        admin_id = random.Next(1, 3)
                    });
            }
            for (int i = 0; i < 20; i++)
            {
                context.notice.AddOrUpdate(
                    new EF.Models.notice()
                    {
                        id = i + 1,
                        title = "Notice " + i,
                        description = "Hi, this is a random notice.\n" + Guid.NewGuid().ToString(),
                        emp_id = random.Next(3, 10)
                    });
            }
            // Bus provider
            for (int i = 11; i < 15; i++)
            {
                context.user.AddOrUpdate(
                    new EF.Models.user()
                    {
                        id = i,
                        username = "bp" + i,
                        password = "123",
                        userRole = "busProvider"
                    });
                context.busProvider.AddOrUpdate(
                    new EF.Models.busProvider()
                    {
                        id = i,
                        company = "Mr. BP " + i,
                        emp_id = random.Next(3, 10)
                    });
            }
            // Bus
            string[] brands = { "Mercedes-Benz", "Volvo", "Scania", "Hunday" };
            string[] categories = { "ac", "non-ac" };
            //int[] seats = { 30, 40, 50, 60 };
            for (int i = 1; i < 10; i++)
            {
                context.bus.AddOrUpdate(
                    new EF.Models.bus()
                    {
                        id = i,
                        brand = brands[random.Next(brands.Count())],
                        model = "model" + random.Next(3),
                        serialNo = Guid.NewGuid().ToString(),
                        totalSeat = 40,
                        category = categories[random.Next(categories.Count())],
                        bp_id = random.Next(11, 15),
                    });
            }
            //places
            string[] places = { "Dhaka", "Chittagong", "Sylhet", "Rajshahi", "Barishal", "Khulna", "Kumilla", "Rangpur" };
            for (int i = 0; i < places.Count(); i++)
            {
                context.place.AddOrUpdate(
                    new EF.Models.place()
                    {
                        id = i + 1,
                        name = places[i],
                        emp_id = random.Next(3, 10)
                    });
            }
            // trips
            for (int i = 0; i < 100; i++)
            {
                DateTime stime = DateTime.Now.Date.AddDays(random.Next(-5, 20)).AddHours(random.Next(0, 24));
                DateTime eTime = stime.AddHours(random.Next(0, 24));
                context.trip.AddOrUpdate(
                    new EF.Models.trip()
                    {
                        id = i + 1,
                        ticketPrice = random.Next(4, 15) * 100,
                        status = "added",
                        startTime = stime,
                        endTime = eTime,
                        depot_id = random.Next(places.Count()) + 1,
                        dest_id = random.Next(places.Count()) + 1,
                        //emp_id = random.Next(3, 10),
                        bus_id = random.Next(1, 10),

                    });
            }
            // Customer
            for (int i = 16; i < 20; i++)
            {
                context.user.AddOrUpdate(
                    new EF.Models.user()
                    {
                        id = i,
                        username = "customer" + i,
                        password = "123",
                        userRole = "customer"
                    });
                context.customer.AddOrUpdate(
                    new EF.Models.customer()
                    {
                        id = i,
                        name = "Mr. Customer " + i,
                    });
            }
            context.SaveChanges();
        }
    }
}