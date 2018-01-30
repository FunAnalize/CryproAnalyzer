using System;
using System.Linq;
using AnalysisTools.Indicators.MovingAverageIndicator;
using AnalysisTools.Lines;
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
            var botClient = new BotClient(Tokens.TelegramToken);
            botClient.Start();

            var signalMailer = new SignalMailer();
            signalMailer.Start();
        }
    }
}