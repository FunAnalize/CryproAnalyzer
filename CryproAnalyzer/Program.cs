using Bittrex.Net;
using CryproAnalyzer.Models;
using CryproAnalyzer.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace CryproAnalyzer
{
    public static class Program
    {
        private const string BittrexKey = "bdccc7011d0149abba1b7259f9fab600";
        private const string BittrexSecret = "bba32eed2e2341d69ff344f9640f21c4";

        private const string TelegramToken = "517696499:AAFd655vCUB51agOd9_jrepHQXoHdbDvvCQ";


        public static void Main(string[] args)
        {
            BotClient bc = new BotClient(TelegramToken);
            bc.Start();
            while (true)
            {
                
            }
        }
    }
}