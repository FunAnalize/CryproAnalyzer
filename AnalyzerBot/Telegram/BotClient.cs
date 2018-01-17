using System.Data.Entity;
using System.Linq;
using System.Threading;
using Models.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using User = Models.Models.User;

namespace CryproAnalyzer.Telegram
{
    internal class BotClient
    {
        private readonly TelegramBotClient _botClient;
        private readonly Thread _thread;
        private int _offset;

        public BotClient(string telegramToken)
        {
            _thread = new Thread(Act);
            _botClient = new TelegramBotClient(telegramToken);
        }

        public void Start()
        {
            _thread.Start();
        }

        public void Stop()
        {
            _thread.Interrupt();
        }

        private void Act()
        {
            while (true)
            {
                var updates = _botClient.GetUpdatesAsync(offset: _offset, timeout: 100);

                foreach (var update in updates.Result)
                {
                    _offset = update.Id + 1;

                    if (update.Message.Type == MessageType.TextMessage)
                    {
                        var chatId = update.Message.Chat.Id;
                        var messageText = update.Message.Text;

                        bool? isSubscribe = null;
                        var responseText = "";
                        IReplyMarkup replyMarkup = null;

                        switch (messageText)
                        {
                            case "/start":
                                responseText = "Добро пожаловать в чат-бот. Подпишься на сигналы.";
                                replyMarkup = new ReplyKeyboardMarkup(new[]
                                        {new KeyboardButton("Подписаться")},
                                    resizeKeyboard: true);
                                isSubscribe = false;
                                break;
                            case "Подписаться":
                                responseText = "Спасибо за подписку!";
                                replyMarkup = new ReplyKeyboardMarkup(new[]
                                        {new KeyboardButton("Отписаться")},
                                    resizeKeyboard: true);
                                isSubscribe = true;
                                break;
                            case "Отписаться":
                                responseText = "Ты всегда можешь подписаться снова!";
                                replyMarkup = new ReplyKeyboardMarkup(new[]
                                        {new KeyboardButton("Подписаться")},
                                    resizeKeyboard: true);
                                isSubscribe = false;
                                break;
                        }

                        _botClient.SendTextMessageAsync(chatId, responseText,
                            replyMarkup: replyMarkup);

                        if (!(isSubscribe is null))
                        {
                            DbUpdate(chatId, isSubscribe.Value);
                        }
                    }
                }
            }
        }

        private static void DbUpdate(long chatId, bool isSubscribe)
        {
            using (var db = new AnalyzerContext())
            {
                var user = db.Users.FirstOrDefault(p => p.ChatId == chatId);
                if (user is null)
                {
                    db.Users.Add(new User { ChatId = chatId, IsSubscribed = isSubscribe });
                }
                else
                {
                    user.IsSubscribed = isSubscribe;
                    db.Entry(user).State = EntityState.Modified;
                }

                db.SaveChanges();
            }
        }
    }
}