namespace BotKeeper.Service {
    using BotKeeper.Service.Core;
    using BotKeeper.Service.Core.States;
    using BotKeeper.Service.Interfaces;
	using System;
	using Telegram.Bot;
	using Telegram.Bot.Args;
	internal class ApplicationBot {

		private readonly TelegramBotClient client;
		private readonly IStorage storage;

		public ApplicationBot(
			TelegramBotClient client,
			IStorage storage) {
			this.client = client ?? throw new ArgumentNullException(nameof(client));
			this.storage = storage;
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
			var context = new Context(new GuestState(), storage, client);

			var isUserExist = context.UserService.IsUserExist(messageEventArgs.Message.Chat.Id);
			if (isUserExist) {
				context.TransitionTo(new MemberState());
			}

			context.InitialState(messageEventArgs);
		}
	}
}