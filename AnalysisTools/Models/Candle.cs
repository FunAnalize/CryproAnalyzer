using System;

namespace AnalysisTools.Models
{
    public class Candle
    {
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }
        public decimal BaseVolume { get; set; }
        public DateTime Timestamp { get; set; }
    }
}