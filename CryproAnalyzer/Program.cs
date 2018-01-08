using Bittrex.Net;
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
            var botClient = new TelegramBotClient(TelegramToken);
            var bittrexClient = new BittrexClient(BittrexKey, BittrexSecret);


            var markets = bittrexClient.GetMarkets();

            var offset = 0;

            while (true)
            {
                var updates = botClient.GetUpdatesAsync(offset: offset, timeout: 100);

                foreach (var update in updates.Result)
                {
                    offset = update.Id + 1;

                    if (update.Message.Type == MessageType.TextMessage)
                    {
                        var chatId = update.Message.Chat.Id;
                        var messageText = update.Message.Text;


                        var responseText = "";
                        IReplyMarkup replyMarkup = null;

                        switch (messageText)
                        {
                            case "/start":
                                responseText = "Добро пожаловать в чат-бот. Подпишься на сигналы.";
                                replyMarkup = new ReplyKeyboardMarkup(new[]
                                        {new KeyboardButton("Подписаться")},
                                    resizeKeyboard: true);
                                break;
                            case "Подписаться":
                                responseText = "Спасибо за подписку!";
                                replyMarkup = new ReplyKeyboardMarkup(new[]
                                        {new KeyboardButton("Отписаться")},
                                    resizeKeyboard: true);
                                break;
                            case "Отписаться":
                                responseText = "Ты всегда можешь подписаться снова!";
                                replyMarkup = new ReplyKeyboardMarkup(new[]
                                        {new KeyboardButton("Подписаться")},
                                    resizeKeyboard: true);
                                break;
                        }

                        botClient.SendTextMessageAsync(chatId, responseText,
                            replyMarkup: replyMarkup);
                    }
                }
            }
        }
    }
}