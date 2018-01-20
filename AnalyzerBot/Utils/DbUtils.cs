using Microsoft.EntityFrameworkCore;
using Models;

namespace AnalyzerBot.Utils
{
    public class DbUtils
    {

        private static DbContextOptions<AnalyzerContext> _options;
        public static AnalyzerContext GetAnalyzerContext()
        {
            if (_options == null)
            {
                var optionsBuilder = new DbContextOptionsBuilder<AnalyzerContext>();
                _options = optionsBuilder
                    .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=AnalyzerDb;Trusted_Connection=True;MultipleActiveResultSets=True;")
                    .Options;
            }

            return new AnalyzerContext(_options);
        }
    }
}
