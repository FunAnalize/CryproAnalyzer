using CryproAnalyzer.Telegram;

namespace CryproAnalyzer
{
    public static class Program
    {
        private const string BittrexKey = "bdccc7011d0149abba1b7259f9fab600";
        private const string BittrexSecret = "bba32eed2e2341d69ff344f9640f21c4";

        private const string TelegramToken = "517696499:AAFd655vCUB51agOd9_jrepHQXoHdbDvvCQ";


        public static void Main(string[] args)
        {
            var botClient = new BotClient(TelegramToken);
            botClient.Start();
        }
    }
}