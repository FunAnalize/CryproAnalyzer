using System;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class AnalyzerContext : DbContext
    {
        public AnalyzerContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
