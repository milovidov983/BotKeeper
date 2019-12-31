namespace BotKeeper.Service {
	using BotKeeper.Service.Interfaces;
	using System;
	using Telegram.Bot;
	using Telegram.Bot.Args;
	internal class ApplicationTelegramBot {

		private readonly TelegramBotClient client;
		private readonly IUserFactory userFactory;
		private readonly IInteractorFactory interactorFactory;
		private readonly IMessageFactory messageFactory;
		private readonly IBotClient botClient;

		public ApplicationTelegramBot(
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
			client.OnMessage += BotOnMessageReceived;
			client.OnMessageEdited += BotOnMessageReceived;
			client.StartReceiving();
			Console.WriteLine("Bot started...");
			Console.ReadLine();
			client.StopReceiving();
		}

		private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs) {
			IUser user = await userFactory.Create(messageEventArgs.Message.From.Id);
			IInteractor interactor = interactorFactory.Create(user, botClient);
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
