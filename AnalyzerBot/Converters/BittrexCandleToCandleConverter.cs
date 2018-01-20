using System.Collections.Generic;
using System.Linq;
using AnalysisTools.Models;
using Bittrex.Net.Objects;

namespace AnalyzerBot.Converters
{
    public class BittrexCandleToCandleConverter
    {
        public List<Candle> Convert(IEnumerable<BittrexCandle> candles)
        {
            /*
             * Данный метод конвертирует свечи полученные с битрикса в собственный тип свечей. Это нужно для более простого перехода в дальнейшем на другие биржы.
             */
            return candles.Select(candle => new Candle
            {
                Low = candle.Low,
                High = candle.High,
                Timestamp = candle.Timestamp,
                BaseVolume = candle.BaseVolume,
                Close = candle.Close,
                Open = candle.Open,
                Volume = candle.Volume,
            }).ToList();
        }
    }
}
