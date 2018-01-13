using CryproAnalyzer.Telegram;

namespace CryproAnalyzer
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