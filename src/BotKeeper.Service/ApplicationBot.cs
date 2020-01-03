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
		private readonly IParserService parserService;
		private readonly IHandlerService handlerService;
		private readonly IContextFactory contextFactory;
		private readonly ILogger logger;

		public ApplicationBot(
			TelegramBotClient client,
			IStorage storage) {
			logger = Settings.Logger;

			this.client = client ?? throw new ArgumentNullException(nameof(client));

			var sender = new SenderService(client);
			serviceFactory = new ServiceFactory(storage, sender, Settings.Logger);
			parserService = serviceFactory.ParserService;
			handlerService = serviceFactory.HandlerService;
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
			var command = parserService.Parse(request.Message.Text);

			await handlerService.HandleCommand(request, context, command);
		}




		public void Dispose() {
			Stop();
		}
		public void Stop() {
			client?.StopReceiving();
		}
	}
}