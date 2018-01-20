using System.Collections.Generic;
using System.Linq;
using AnalysisTools.Models;

namespace AnalysisTools.Indicators.FractalIndicator
{
    public class FractalIndicator
    {
        public List<FractalIndicatorResult> Process(List<Candle> candles, int period = 15)
        {
            /*
             * Данный метод используется для поиска фракталов (локальных максимумов и минимумов) на свечном графике.
             * На вход принимает свечи для анализа, а так же периода для анализа, количество свечей лежащих рядом с целевой свечей, который будут учитываться при анализе.
             * Оптимально использовать период от 15 до 20.
             */
            period = period % 2 == 0 ? period + 1 : period;

            var result = new List<FractalIndicatorResult>();

            for (var i = 0; i < candles.Count - period; i++)
            {
                var fractalIndicatorResult = ProcessPeriod(candles.GetRange(i, period), period);

                if (fractalIndicatorResult.FractalIndicatorType != FractalIndicatorType.Neutral)
                {
                    result.Add(fractalIndicatorResult);
                }
            }

            return result;
        }

        private static FractalIndicatorResult ProcessPeriod(IList<Candle> periodCandles, int period)
        {
            var targetCandle = periodCandles[period / 2 + 1];

            var fractalIndicatorType = FractalIndicatorType.Neutral;

            if (IsRisingFractal(periodCandles, targetCandle))
            {
                fractalIndicatorType = FractalIndicatorType.Rising;
            }

            if (IsFallingFractal(periodCandles, targetCandle))
            {
                fractalIndicatorType = FractalIndicatorType.Falling;
            }

            return new FractalIndicatorResult
            {
                Candle = targetCandle,
                FractalIndicatorType = fractalIndicatorType
            };
        }

        private static bool IsRisingFractal(IEnumerable<Candle> periodCandles, Candle targetCandle)
        {
            return periodCandles.FirstOrDefault(candle => candle.High > targetCandle.High) == null;
        }

        private static bool IsFallingFractal(IEnumerable<Candle> periodCandles, Candle targetCandle)
        {
            return periodCandles.FirstOrDefault(candle => candle.Low < targetCandle.Low) == null;
        }
    }
}