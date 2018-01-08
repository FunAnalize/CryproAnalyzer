using System;
using System.Linq;
using Bittrex.Net;
using Bittrex.Net.Objects;

namespace CryproAnalyzer.Analyzers
{
    class LowerAvergeAnalyzer
    {
        private readonly BittrexClient _client;

        public LowerAvergeAnalyzer(BittrexClient client)
        {
            _client = client;
        }

        public LowerAvergeAnalyzerResult Analyze(string marketName,int dayInterval)
        {
            try
            {
                decimal result=0m;
                int index = 0;
                var candels = _client.GetCandles(marketName, TickInterval.HalfHour).Result;
                foreach (var candle in candels.Where(p=>p.Timestamp>DateTime.Now.AddDays(-dayInterval)))
                {
                    result+=(candle.Open + candle.Close) / 2;
                    index++;
                }

                var currentPrice = _client.GetTicker(marketName).Result.Bid;
                var average = result / index;
                if (currentPrice < average)
                {
                    return new LowerAvergeAnalyzerResult
                        { MarketName = marketName,Average = average,Current = currentPrice,GoodBuy = true,Percent = average/currentPrice*100};

                }
                return new LowerAvergeAnalyzerResult
                    { MarketName = marketName, Average = average, Current = currentPrice,GoodBuy = false, Percent = average / currentPrice*100 };
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return new LowerAvergeAnalyzerResult();
            }
        }
    }

    class LowerAvergeAnalyzerResult
    {
        public decimal Average { get; set; }
        public decimal Current { get; set; }
        public string MarketName { get; set; }
        public bool GoodBuy { get; set; }
        public decimal Percent { get; set; }
    }
}
