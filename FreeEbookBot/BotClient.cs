using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;

namespace FreeEbookBot
{
    public sealed class BotClient : TelegramBotClient
    {
        private static readonly Lazy<BotClient> lazy =
            new Lazy<BotClient>(() => new BotClient("349101883:AAHSMtmyHNCej__A9RobXbbMiAdTP6pMnMc"));

        public static BotClient Instance { get { return lazy.Value; } }

        private BotClient(string token)
        : base(token)
        {
        }

    }
}
