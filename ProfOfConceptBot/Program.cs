using System;

namespace ProfOfConceptBot {
    using System.Threading.Tasks;
    using Telegram.Bot;
	using Telegram.Bot.Args;
	using Telegram.Bot.Types;
	using Telegram.Bot.Types.Enums;
	interface IUser { }
	interface IInteractor {
		Task Execute(IMessage message);
	}
	interface IMessage { }
	interface IBotClient { }
	interface IMessageFactory {
		IMessage Create(MessageEventArgs messageEventArgs);
	}
	interface IUserFactory {
		Task<IUser> Create(int id);
	}
	interface IInteractorFactory {
		Task<IInteractor> Create(IUser user, IBotClient client);
	}
	class Program {
		private static TelegramBotClient client;
		private static IUserFactory userFactory;
		private static IInteractorFactory interactorFactory;
		private static IMessageFactory messageFactory;
		private static IBotClient botClient;
		

		static void Main(string[] args) {

			client = new TelegramBotClient("");
			client.OnMessage += BotOnMessageReceived;
			client.OnMessageEdited += BotOnMessageReceived;
			client.StartReceiving();
			Console.WriteLine("Bot started...");
			Console.ReadLine();
			client.StopReceiving();
		}
		private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs) {
			IUser user = await userFactory.Create(messageEventArgs.Message.From.Id).ConfigureAwait(false);
			IInteractor interactor = await interactorFactory.Create(user, botClient).ConfigureAwait(false);
			IMessage message = messageFactory.Create(messageEventArgs);

			await interactor.Execute(message).ConfigureAwait(false);


			//var message = messageEventArgs.Message;
			//if (message?.Type == MessageType.Text) {

			//	await client.DeleteMessageAsync(message.Chat.Id, message.MessageId).ConfigureAwait(false);

			//	await client.SendTextMessageAsync(message.Chat.Id, message.Text);
			//}
		}



	}
}
