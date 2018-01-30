using System;
using Microsoft.EntityFrameworkCore;
using Models.Migrations;

namespace Models
{
    public class AnalyzerContext : DbContext
    {

        public AnalyzerContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Signal> Signals { get; set; }
        public DbSet<SignalResult> SignalResults { get; set; }
    }
}
