using AnalysisTools.Models;

namespace AnalysisTools.Indicators.FractalIndicator
{
    public class FractalIndicatorResult
    {
        public FractalIndicatorType FractalIndicatorType { get; set; }
        public Candle Candle { get; set; }
        public decimal Price { get; set; }
    }
}
