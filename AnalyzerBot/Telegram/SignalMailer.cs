using System;
using System.Linq;
using System.Threading;
using AnalyzerBot.Analyzers;
using AnalyzerBot.Utils;
using Bittrex.Net;
using Models;
using Telegram.Bot;

namespace AnalyzerBot.Telegram
{
    internal class SignalMailer
    {
        private readonly Thread _thread;
        private static int _timeInterval = 15;

        public SignalMailer()
        {
            _thread = new Thread(Act);
        }

        public void Start()
        {
            _thread.Start();
        }

        public void Stop()
        {
            _thread.Interrupt();
        }

        private static void Act()
        {
            var botClient = new TelegramBotClient(Tokens.TelegramToken);
            var bittrexClient = new BittrexClient(Tokens.BittrexKey, Tokens.BittrexSecret);

            var glassAnalyzer = new GlassAnalyzer(bittrexClient);
            var lowerAvergeAnalyzer = new LowerAvergeAnalyzer(bittrexClient);

            while (true)
            {
                var markets = bittrexClient.GetMarketsAsync().Result;

                foreach (var bittrexMarket in markets.Result)
                {
                    if (!bittrexMarket.MarketName.StartsWith("BTC"))
                    {
                        continue;
                    }

                    var lowerAvergeAnalyzerResult = lowerAvergeAnalyzer.Analyze(bittrexMarket.MarketName, _timeInterval);

                    var glassAnalyzerResult = glassAnalyzer.Analyze(bittrexMarket.MarketName);

                    if (lowerAvergeAnalyzerResult is null || glassAnalyzerResult is null)
                    {
                        continue;
                    }

                    DbUpdate(_timeInterval, lowerAvergeAnalyzerResult.MarketName, lowerAvergeAnalyzerResult.Current,
                        lowerAvergeAnalyzerResult.Average, glassAnalyzerResult.Ratio);

                    Console.WriteLine("Market: {0} Avarge: {1}, Current: {2}, Percent: {3}, GoodBuy: {4}, Ratio: {5}",
                        lowerAvergeAnalyzerResult.MarketName,
                        lowerAvergeAnalyzerResult.Average,
                        lowerAvergeAnalyzerResult.Current,
                        lowerAvergeAnalyzerResult.Percent,
                        lowerAvergeAnalyzerResult.GoodBuy,
                        glassAnalyzerResult.Ratio);


                    if (lowerAvergeAnalyzerResult.Percent <= 10 || glassAnalyzerResult.Ratio < 0.65m) continue;

                    using (var db = DbUtils.GetAnalyzerContext())
                    {
                        var users = db.Users.Where(user => user.IsSubscribed);
                        foreach (var user in users)
                        {
                            botClient.SendTextMessageAsync(user.ChatId,
                                "Маркет: " + bittrexMarket.MarketName + "\n" +
                                "Отклонение  от среднего: " + lowerAvergeAnalyzerResult.Percent + "\n" +
                                "Коэффициент оредеров: " + glassAnalyzerResult.Ratio + "\n" +
                                "Текущая цена: " + lowerAvergeAnalyzerResult.Current + "\n" +
                                "https://bittrex.com/Market/Index?MarketName=" + bittrexMarket.MarketName);
                        }
                    }
                }

                Thread.Sleep(600000);
            }
        }

        private static void DbUpdate(int intervalToSearch, string marketName, decimal price, decimal average, decimal? ratio)
        {
            if (!CheckExistSignal(intervalToSearch, marketName))
            {
                DbCreateSignal(marketName,price,average,ratio,intervalToSearch);
            }
        }

        private static void DbCreateSignal(string marketName,decimal price,decimal average, decimal? ratio,int dayInterval)
        {
            using (var db = DbUtils.GetAnalyzerContext())
            {
                var signal = new Signal
                {
                    MarketName = marketName,
                    Price = price,
                    Interval = dayInterval,
                    Average = average,
                    Ratio = ratio,
                    Timestamp = DateTime.Now
                };
                db.Signals.Add(signal);
                db.SaveChanges();
            }
        }

        private static bool CheckExistSignal(int intervalToSearch, string marketName)
        {
            using (var db = DbUtils.GetAnalyzerContext())
            {
                var signal = db.Signals.FirstOrDefault(p =>
                    p.MarketName == marketName && p.Timestamp > DateTime.Now.AddDays(-intervalToSearch));
                return signal != null;
            }
        }
    }
}