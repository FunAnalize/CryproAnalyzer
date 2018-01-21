using System.Collections.Generic;
using System.Linq;
using AnalysisTools.Models;

namespace AnalysisTools.Indicators.MovingAverageIndicator
{
    public class MovingAverageIndicator
    {

        public List<MovingAverageIndicatorResult> Process(List<Candle> candles, int period = 15)
        {
            var movingAverageIndicatorResults = new List<MovingAverageIndicatorResult>();

            for (var i = 1; i < candles.Count; i++)
            {
                var startIndex = i - period < 1 ? 1 : i - period;
                var candleCount = startIndex == 1 ? i : period;
                var avergePrice = candles.GetRange(startIndex, period).Sum(candle => candle.Close) / candleCount;
                movingAverageIndicatorResults.Add(new MovingAverageIndicatorResult
                {
                    Price = avergePrice,
                    Timestamp = candles[i].Timestamp
                });
            }

            return movingAverageIndicatorResults;
        }
    }
}
