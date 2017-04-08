using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace FreeEbookBot
{
    class Program
    {
        static TelegramBotClient Bot = BotClient.Instance;
        private static Timer _timer = new Timer(CheckNewBookAsync, null, 0, Timeout.Infinite);
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Bot.OnCallbackQuery += Bot_OnCallbackQuery;
            Bot.OnMessage += Bot_OnMessage; ;
            Bot.OnInlineResultChosen += Bot_OnInlineResultChosen; ;
            Bot.OnReceiveError += Bot_OnReceiveError;
            Console.ReadLine();
        }

        private static void Bot_OnReceiveError(object sender, Telegram.Bot.Args.ReceiveErrorEventArgs e)
        {
            try
            {
                StaticUtils.Logger.Error("OnReceiveError:{0} {1}", e.ApiRequestException.Message, e.ApiRequestException.StackTrace);
            }
            catch (Exception ex)
            {
                StaticUtils.Logger.Error("{0} {1}", ex.Message, ex.StackTrace);
            }
            
        }

        private static void Bot_OnInlineResultChosen(object sender, Telegram.Bot.Args.ChosenInlineResultEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                StaticUtils.Logger.Error("{0} {1}", ex.Message, ex.StackTrace);
            }
        }

        private static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                StaticUtils.Logger.Error("{0} {1}", ex.Message, ex.StackTrace);
            }
        }

        private static void Bot_OnCallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                StaticUtils.Logger.Error("{0} {1}", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Проверяем, есть ли новая книга (проверяем раз в сутки)
        /// </summary>
        /// <param name="state"></param>
        private static void CheckNewBookAsync(object state)
        {
            try
            {
                var crawler = new Crawler();
                //crawler.ParseWikiTitleAsync();
                var book=crawler.ParsePactbAsync();
            }
            catch (Exception ex)
            {
                StaticUtils.Logger.Error("{0} {1}", ex.Message, ex.StackTrace);
            }
        }
    }
}
