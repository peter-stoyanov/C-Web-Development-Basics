using ShopHierarchy.DAL;
using ShopHierarchy.Models;
using System;
using System.Linq;

namespace ShopHierarchy
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var db = new ShopHierarchyDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                ReadSalesmen(db);
                ReadItems(db);
                //ReadCustomers(db);
                //ReadCustomersWithOrdersReviews(db);
                ReadCustomersWithOrdersReviewsExtended(db);

                //PrintNumberOfCustomersPerSalesman(db);
                //PrintCustomerStats(db);
                //PrintFullClientStats(db);
                //PrintFullClientStatsTask8(db);
                PrintFullClientStatsTask9(db);
            }
        }

        private static void PrintFullClientStatsTask8(ShopHierarchyDbContext db)
        {
            int customerId = int.Parse(Console.ReadLine());

            var customerStats = db.Customers
                .Where(c => c.Id == customerId)
                .Select
                (c => new
                {
                    Name = c.Name,
                    OrdersCount = c.Orders.Count(),
                    ReviewsCount = c.Reviews.Count(),
                    SalesmanName = c.Salesman.Name
                })
                .FirstOrDefault();

            Console.WriteLine($"Customer: {customerStats?.Name}");
            Console.WriteLine($"Orders count: {customerStats?.OrdersCount}");
            Console.WriteLine($"Reviews count: {customerStats?.ReviewsCount}");
            Console.WriteLine($"Salesman: {customerStats?.SalesmanName}");
        }

        private static void PrintFullClientStatsTask9(ShopHierarchyDbContext db)
        {
            int customerId = int.Parse(Console.ReadLine());

            var customerStats = db.Customers
                .Where(c => c.Id == customerId)
                .Select
                (c => new
                {
                    Orders = c.Orders.Where(or => or.Items.Count() > 1).Count()
                })
                .FirstOrDefault();

            Console.WriteLine($"Orders: {customerStats.Orders}");
        }

        private static void PrintFullClientStats(ShopHierarchyDbContext db)
        {
            int customerId = int.Parse(Console.ReadLine());

            var reportEntry = db.Customers
                .Where(c => c.Id == customerId)
                .Select(c => new
                {
                    Orders = c.Orders.Select(or => new
                    {
                        id = or.Id,
                        itemsCount = or.Items.Count()
                    })
                        .ToList(),
                    ReviewsCount = c.Reviews.Count()
                })
                .FirstOrDefault();

            foreach (var entry in reportEntry.Orders)
            {
                Console.WriteLine($"order {entry.id}: {entry.itemsCount} items");
            }
            Console.WriteLine($"reviews: {reportEntry.ReviewsCount}");
        }

        private static void ReadItems(ShopHierarchyDbContext db)
        {
            while (true)
            {
                var input = Console.ReadLine();

                if (input == "END") { break; }

                var tokens = input
                    .Split(";", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                db.Items.Add(new Item() { Name = tokens[0], Price = decimal.Parse(tokens[1]) });
            }

            db.SaveChanges();
        }

        private static void PrintCustomerStats(ShopHierarchyDbContext db)
        {
            var customerStats = db.Customers
                .Select(c => new
                {
                    Name = c.Name,
                    OrdersCount = c.Orders.Count(),
                    ReviewsCount = c.Reviews.Count()
                })
                .OrderByDescending(stat => stat.OrdersCount)
                .ThenByDescending(stat => stat.ReviewsCount)
                .ToList();

            var line = Environment.NewLine;
            foreach (var stat in customerStats)
            {
                Console.WriteLine($"{stat.Name}{line}Orders: {stat.OrdersCount}{line}Reviews: {stat.ReviewsCount}");
            }
        }

        private static void ReadCustomersWithOrdersReviews(ShopHierarchyDbContext db)
        {
            while (true)
            {
                var input = Console.ReadLine();

                if (input == "END") { break; }

                var commands = input.Split('-').ToArray();
                switch (commands[0])
                {
                    case "register":
                        var tokens = commands[1].Split(';');
                        string name = tokens[0];
                        int salesmanId = int.Parse(tokens[1]);
                        db.Customers.Add(new Customer() { Name = name, SalesmanId = salesmanId });
                        break;

                    case "order":
                        int customerId = int.Parse(commands[1]);
                        db.Orders.Add(new Order() { CustomerId = customerId });
                        break;

                    case "review":
                        customerId = int.Parse(commands[1]);
                        db.Reviews.Add(new Review() { CustomerId = customerId });
                        break;
                }
            }

            db.SaveChanges();
        }

        private static void ReadCustomersWithOrdersReviewsExtended(ShopHierarchyDbContext db)
        {
            while (true)
            {
                var input = Console.ReadLine();

                if (input == "END") { break; }

                var commands = input.Split('-').ToArray();
                switch (commands[0])
                {
                    case "register":
                        var tokens = commands[1].Split(';');
                        string name = tokens[0];
                        int salesmanId = int.Parse(tokens[1]);
                        db.Customers.Add(new Customer() { Name = name, SalesmanId = salesmanId });
                        break;

                    case "order":
                        var ids = commands[1].Split(';', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                        var newOrder = new Order() { CustomerId = ids[0] };
                        foreach (var itemId in ids.Skip(1))
                        {
                            newOrder.Items.Add(new ItemOrder() { ItemId = itemId, OrderId = ids[0] });
                        }
                        db.Orders.Add(newOrder);
                        break;

                    case "review":
                        ids = commands[1].Split(';', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                        db.Reviews.Add(new Review() { CustomerId = ids[0], ItemId = ids[1] });
                        break;
                }
            }

            db.SaveChanges();
        }

        private static void PrintNumberOfCustomersPerSalesman(ShopHierarchyDbContext db)
        {
            //// as in lab docs = loading all connected tables is unnecessary
            //var reportData = db.Salesmen
            //    .Include(s => s.Customers)
            //    .OrderByDescending(s => s.Customers.Count())
            //    .ThenBy(s => s.Name)
            //    .ToList();

            var reportdata = db.Salesmen
                .Select(s => new
                {
                    Name = s.Name,
                    Customers = s.Customers.Count()
                })
                .OrderByDescending(entry => entry.Customers)
                .ThenBy(entry => entry.Name)
                .ToList();

            foreach (var entry in reportdata)
            {
                Console.WriteLine($"{entry.Name} - {entry.Customers} customers");
            }
        }

        private static void ReadCustomers(ShopHierarchyDbContext db)
        {
            while (true)
            {
                var input = Console.ReadLine();

                if (input == "END") { break; }

                var tokens = input.Substring(input.IndexOf('-') + 1).Split(';').ToArray();
                string name = tokens[0];
                int salesmanId = int.Parse(tokens[1]);

                db.Customers.Add(new Customer() { Name = name, SalesmanId = salesmanId });

                //// as in lab docs
                //var customer = new Customer() { Name = name };
                //EntityEntry<Customer> customerTracker = db.Customers.Add(customer);
                //var salesman = db.Salesmen.Where(s => s.Id == salesmanId).FirstOrDefault();
                //salesman?.Customers.Add(customerTracker.Entity);
            }

            db.SaveChanges();
        }

        private static void ReadSalesmen(ShopHierarchyDbContext db)
        {
            var salesman = Console.ReadLine()
                                .Split(";", StringSplitOptions.RemoveEmptyEntries)
                                .ToList();

            foreach (var name in salesman)
            {
                db.Salesmen.Add(new Salesman() { Name = name });
            }

            db.SaveChanges();
        }
    }
}