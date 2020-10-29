using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace NagisaGamebot
{
    class Program
    {
        private static ITelegramBotClient botClient;



        static void Main(string[] args)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            botClient = new TelegramBotClient("1257490387:AAH6Fs43y-LALOBilMWyM29AcRAEUk_-VPQ");
            var me = botClient.GetMeAsync().Result;
            Console.WriteLine($"Bot id: {me.Id}. Bot Name: {me.FirstName}");
            botClient.OnMessage += Bot_OnMessage;

            botClient.StartReceiving();
            Console.ReadKey();
            botClient.StopReceiving();
        }
        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {

            var text = e?.Message?.Text;
            if (text == null)
                return;
            Console.WriteLine($"received text message '{text}' in chat '{e.Message.Chat.Id}'");
            switch (text)
            {
                case "/start":
                    await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: "Привет, это мой игровой бот в Telegram.").ConfigureAwait(false);
                    break;
                case "/help":
                    await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: "Список команд").ConfigureAwait(false);
                    break;

                default:
                    await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: "Что ты мне хочешь сказать?").ConfigureAwait(false);
                    await botClient.ForwardMessageAsync(chatId: 751765772, e.Message.Chat, e.Message.MessageId).ConfigureAwait(false);
                    break;
            }
            //   await botClient.SendTextMessageAsync(chatId: e.Message.Chat,text: $"Ты написал '{text}'").ConfigureAwait(false); // Бот отвечает текстовыми сообщениями, что ему написали

        }


    }
}