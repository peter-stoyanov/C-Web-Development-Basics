using System;

namespace StudentsDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new StudentsDbContext())
            {
                db.Database.EnsureCreated();
            }
        }
    }
}
