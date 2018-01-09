using System;
using System.Linq;
using Bittrex.Net;
using Bittrex.Net.Objects;
using CryproAnalyzer.Analyzers.Models;

namespace CryproAnalyzer.Analyzers
{
    internal class LowerAvergeAnalyzer
    {
        private readonly BittrexClient _client;

        public LowerAvergeAnalyzer(BittrexClient client)
        {
            _client = client;
        }

        public LowerAvergeAnalyzerResult Analyze(string marketName, int dayInterval)
        {
            try
            {
                var candels = _client.GetCandles(marketName, TickInterval.HalfHour).Result;

                var daysInInterval = candels.Where(p => p.Timestamp > DateTime.Now.AddDays(-dayInterval)).ToList();
                var index = daysInInterval.Count();
                var result = daysInInterval.Sum(candle => (candle.Open + candle.Close) / 2);

                var currentPrice = _client.GetTicker(marketName).Result.Bid;
                var averagePrice = result / index;

                return new LowerAvergeAnalyzerResult
                {
                    MarketName = marketName,
                    Average = averagePrice,
                    Current = currentPrice,
                    GoodBuy = currentPrice < averagePrice,
                    Percent = (averagePrice / currentPrice - 1) * 100
                };
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return null;
            }
        }
    }
}