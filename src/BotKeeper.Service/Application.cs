using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service {
    using BotKeeper.Service.Interfaces;
    using System;
	using System.Threading.Tasks;
	using Telegram.Bot;
	using Telegram.Bot.Args;
	using Telegram.Bot.Types;
	using Telegram.Bot.Types.Enums;
	public class Application {

		private TelegramBotClient client;
		private IUserFactory userFactory;
		private IInteractorFactory interactorFactory;
		private IMessageFactory messageFactory;
		private IBotClient botClient;

		public Application(
			TelegramBotClient client, 
			IUserFactory userFactory, 
			IInteractorFactory interactorFactory, 
			IMessageFactory messageFactory, 
			IBotClient botClient) {

			this.client = client ?? throw new ArgumentNullException(nameof(client));
			this.userFactory = userFactory ?? throw new ArgumentNullException(nameof(userFactory));
			this.interactorFactory = interactorFactory ?? throw new ArgumentNullException(nameof(interactorFactory));
			this.messageFactory = messageFactory ?? throw new ArgumentNullException(nameof(messageFactory));
			this.botClient = botClient ?? throw new ArgumentNullException(nameof(botClient));
		}

		public void Run() {
			client = new TelegramBotClient("");
			client.OnMessage += BotOnMessageReceived;
			client.OnMessageEdited += BotOnMessageReceived;
			client.StartReceiving();
			Console.WriteLine("Bot started...");
			Console.ReadLine();
			client.StopReceiving();
		}

		private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs) {
			IUser user = await userFactory.Create(messageEventArgs.Message.From.Id);
			IInteractor interactor = await interactorFactory.Create(user, botClient);
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
