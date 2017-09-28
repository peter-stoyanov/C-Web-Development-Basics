using FootballBetting.Data;
using Microsoft.EntityFrameworkCore;

namespace FootballBetting.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var db = new AppDbContext())
            {
                db.Database.Migrate();
            }
        }
    }
}
