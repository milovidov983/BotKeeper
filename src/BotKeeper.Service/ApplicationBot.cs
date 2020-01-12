namespace BotKeeper.Service {
	using BotKeeper.Service.Core;
	using BotKeeper.Service.Core.Interfaces;
	using BotKeeper.Service.Core.Models;
	using BotKeeper.Service.Core.Services;
	using BotKeeper.Service.Core.States;

	using System;
	using System.Threading.Tasks;
	using Telegram.Bot;
	using Telegram.Bot.Args;
	internal class ApplicationBot: IDisposable {

		private readonly TelegramBotClient client;
		private readonly IServiceFactory serviceFactory;
		private readonly IHandlerFactory handlerFactory;
		private readonly IContextFactory contextFactory;
		private readonly ILogger logger;

		public ApplicationBot(
			TelegramBotClient client,
			IStorage storage) {
			logger = Settings.Logger;

			this.client = client ?? throw new ArgumentNullException(nameof(client));

			var sender = new SenderService(client);
			serviceFactory = new ServiceFactory(storage, sender, Settings.Logger);
			handlerFactory = serviceFactory.HandlerFactory;
			contextFactory = serviceFactory.ContextFactory;
		}

		public void Run() {
			client.OnMessage += BotOnMessageReceived;
			client.OnMessageEdited += BotOnMessageReceived;
			client.StartReceiving();
			logger.Info("Receiving started.");
		}


		private async void BotOnMessageReceived(object sender, MessageEventArgs request) {
			var userId = request.Message.From.Id;
			var context = await contextFactory.CreateContext(userId);
			var handler = handlerFactory.GetHandlerForCommand(request.Message.Text);

			handler.Execute(context, request);
		}

		public void Dispose() {
			Stop();
		}
		public void Stop() {
			client?.StopReceiving();
		}
	}
}