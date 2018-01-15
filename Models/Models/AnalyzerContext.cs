using System.Data.Entity;

namespace Models.Models
{
    public class AnalyzerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}