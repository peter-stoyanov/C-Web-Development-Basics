using System;

namespace EmployeesDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new EmployeesDbContext())
            {
                //db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}
