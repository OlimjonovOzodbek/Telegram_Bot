using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Exam_Bot
{
    public class MyBot
    {
        string admin = "+998934013443";
        private string MyToken;
        bool checkcontact = true;
        public MyBot(string token)
        {
            MyToken = token;
        }
        public async Task Begin()
        {
            var botClient = new TelegramBotClient(MyToken);

            using CancellationTokenSource cts = new();

            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            cts.Cancel();

            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {

                var messageText = "";
                if (update.Message is not { } message)
                    return;
                if (message.Type == MessageType.Text)
                {
                    messageText = message.Text;
                }

                var chatId = message.Chat.Id;

                Console.WriteLine($"Received a '{messageText}' \n{chatId}.");

                if (messageText == "/start")
                {
                    ReplyKeyboardMarkup replyKeyboardMarkup = new(
                        new[]
                {
                  KeyboardButton.WithRequestContact("Contact")
                })

                    {
                        ResizeKeyboard = true
                    };

                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "𝐀𝐬𝐬𝐚𝐥𝐨𝐦𝐮 𝐚𝐥𝐞𝐲𝐤𝐮𝐦, 𝐛𝐨𝐭𝐝𝐚𝐧 𝐟𝐨𝐲𝐝𝐚𝐥𝐚𝐧𝐢𝐬𝐡 𝐮𝐜𝐡𝐮𝐧 𝐤𝐨𝐧𝐭𝐚𝐤𝐭𝐧𝐢 𝐣𝐨'𝐧𝐚𝐭𝐢𝐧𝐠",
                        replyMarkup: replyKeyboardMarkup,
                        cancellationToken: cancellationToken);
                    checkcontact = false;

                }
                if (message.Contact is not null)
                {
                    await botClient.SendContactAsync(
                        chatId: chatId,
                        phoneNumber: message.Contact.PhoneNumber,
                        firstName: message.Contact.FirstName,
                        lastName: message.Contact.LastName,
                        cancellationToken: cancellationToken);
                    Console.WriteLine(message.Contact.PhoneNumber);


                    if (message.Chat.Id == 967332332)
                    {
                        await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "You are admin",
                                cancellationToken: cancellationToken);
                        {
                            ReplyKeyboardMarkup replyKeyboardMarkup = new(
                                new[]
                        {
                                new KeyboardButton("Phone Add"),
                                new KeyboardButton("Phone Delete"),
                                new KeyboardButton("Phone Update"),
                                new KeyboardButton("Phone Statistics")
                        })
                            {
                                ResizeKeyboard = true
                            };
                        await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Phone name",
                        replyMarkup: replyKeyboardMarkup,
                        cancellationToken: cancellationToken);

                        }
                    }

                }
                Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
                {
                    var ErrorMessage = exception switch
                    {
                        ApiRequestException apiRequestException
                            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                        _ => exception.ToString()
                    };

                    Console.WriteLine(ErrorMessage);
                    return Task.CompletedTask;
                }
            }
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
