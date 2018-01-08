using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryproAnalyzer.Analyzers.Models
{
    internal class LowerAvergeAnalyzerResult
    {
        public decimal Average { get; set; }
        public decimal Current { get; set; }
        public string MarketName { get; set; }
        public bool GoodBuy { get; set; }
        public decimal Percent { get; set; }
    }
}