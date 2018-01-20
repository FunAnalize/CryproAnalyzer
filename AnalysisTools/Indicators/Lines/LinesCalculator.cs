using System.Collections.Generic;
using System.Linq;
using AnalysisTools.Models;

namespace AnalysisTools.Indicators.Lines
{
    public class LinesCalculator
    {
        public List<Line> Calculate(List<Candle> candles, int fractalPeriod = 10, int countOfStep = 40)
        {
            var fractalIndicator = new FractalIndicator.FractalIndicator();
            var fractalIndicatorResults = fractalIndicator.Process(candles, fractalPeriod);

            var maxPrice = fractalIndicatorResults.Max(fractal => fractal.Price);
            var priceStep = maxPrice / countOfStep;

            var lines = new List<Line>();

            foreach (var fractal in fractalIndicatorResults)
            {
                var lineLevel = fractal.Price / priceStep;

                var targetLine =
                    lines.FirstOrDefault(line => line.LowPrice < fractal.Price && line.HighPrice > fractal.Price);

                if (targetLine == null)
                {
                    targetLine = new Line
                    {
                        HighPrice = priceStep * (lineLevel + 1),
                        LowPrice = priceStep * lineLevel,
                    };

                    lines.Add(targetLine);
                }

                targetLine.VertexList.Add(fractal.Price);
            }

            foreach (var line in lines)
            {
                line.HighPrice = line.VertexList.Min();
                line.LowPrice = line.VertexList.Max();
            }

            return lines.Where(line => line.VertexList.Count >= 2).OrderByDescending(line => line.LowPrice).ToList();
        }
    }
}