using System.Linq;
using AnalyzerBot.Analyzers.Models;
using Bittrex.Net;

namespace AnalyzerBot.Analyzers
{
    internal class GlassAnalyzer
    {
        private readonly BittrexClient _bittrexClient;

        public GlassAnalyzer(BittrexClient bittrexClient)
        {
            _bittrexClient = bittrexClient;
        }

        public GlassAnalyzerResult Analyze(string marketName)
        {
            var orderBook = _bittrexClient.GetOrderBookAsync(marketName).Result.Result;

            if (orderBook.Buy == null || orderBook.Sell == null)
            {
                return null;
            }

            var buySum = orderBook?.Buy.Sum(order => order.Quantity * order.Rate);
            var sellSum = orderBook?.Sell.Sum(order => order.Quantity * order.Rate);

            return new GlassAnalyzerResult() {MarketName = marketName, Ratio = buySum / sellSum};
        }
    }
}