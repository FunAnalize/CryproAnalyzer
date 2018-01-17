namespace AnalyzerBot.Analyzers.Models
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