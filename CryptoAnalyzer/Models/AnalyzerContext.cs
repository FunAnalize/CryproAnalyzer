using System.Data.Entity;

namespace CryproAnalyzer.Models
{
    internal class AnalyzerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}