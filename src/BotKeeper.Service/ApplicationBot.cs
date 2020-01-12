namespace BotKeeper.Service {
	using BotKeeper.Service.Core;
    using BotKeeper.Service.Core.Factories;
    using BotKeeper.Service.Core.Helpers;
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
		private readonly IStratagyRepository handlerFactory;
		private readonly IContextFactory contextFactory;
		private readonly ILogger logger;

		public ApplicationBot(TelegramBotClient client, IStorage storage) {
			this.client = client ?? throw new ArgumentNullException(nameof(client));

			var metricFactory = new MetricsFactory(Settings.Instance.Env, Settings.Logger);
			var metricService = metricFactory.CreateMetricsService();
			var sender = new SenderService(client, metricService);

			serviceFactory = new ServiceFactory(storage, sender, Settings.Logger);
			handlerFactory = serviceFactory.HandlerFactory;
			contextFactory = serviceFactory.ContextFactory;

			logger = Settings.Logger;
		}

		public void Run() {
			client.OnMessage += BotOnMessageReceived;
			client.OnMessageEdited += BotOnMessageReceived;
			client.StartReceiving();
			logger.Info("Receiving started.");
		}


		private async void BotOnMessageReceived(object sender, MessageEventArgs request) {
			logger.Trace($"Message received {request.Message.Text}");

			try {
				var userId = request.GetUserId();
				var context = await contextFactory.CreateContext(userId);
				var userTextMessage = request.GetClearedTextMessage();
				var handler = handlerFactory.GetStratagyForCommand(userTextMessage);

				handler.Execute(context, request);
				logger.Trace($"Message processed  {request.Message.Text}");
				return;

			} catch (BotException botException) {
				OnError(request, botException);
			} catch (Exception exception) {
				var internalException = BotException.CreateInternalException(exception);
				OnError(request, internalException);
			}

			logger.Trace($"Message failed {request.Message.Text}");
		}



		private void OnError(MessageEventArgs request, BotException ex) {
			if (ex.StatusCode == StatusCodes.InternalError) {
				logger.Error(ex, request.ToJson());
			} else {
				logger.Warn(ex, request.ToJson());
			}

			/// Some additional logic
		}


		public void Dispose() {
			Stop();
		}
		public void Stop() {
			client?.StopReceiving();
		}
	}
}