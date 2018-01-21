using System;
using System.Linq;
using AnalysisTools.Indicators.Lines;
using AnalysisTools.Indicators.MovingAverageIndicator;
using AnalyzerBot.Converters;
using AnalyzerBot.Telegram;
using Bittrex.Net;
using Bittrex.Net.Objects;

namespace AnalyzerBot
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var bittrexClient = new BittrexClient(Tokens.BittrexKey, Tokens.BittrexSecret);
            var bittrexCandles = bittrexClient.GetCandles("BTC-EMC2", TickInterval.HalfHour).Result.Where(candle => DateTime.Now - candle.Timestamp < TimeSpan.FromDays(15));
            var candles = new BittrexCandleToCandleConverter().Convert(bittrexCandles);
            var lines = new LinesCalculator().Calculate(candles);

            var movingAverageIndicatorResults = new MovingAverageIndicator().Process(candles);

            Console.ReadKey();
            //var botClient = new BotClient(Tokens.TelegramToken);
            //botClient.Start();

            //var signalMailer = new SignalMailer();
            //signalMailer.Start();
        }
    }
}