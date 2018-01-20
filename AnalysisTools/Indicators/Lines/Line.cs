using System;
using System.Collections.Generic;
using System.Text;

namespace AnalysisTools.Indicators.Lines
{
    public class Line
    {
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public string Market { get; set; }
        public List<decimal> VertexList { get; set; } = new List<decimal>();
    }
}