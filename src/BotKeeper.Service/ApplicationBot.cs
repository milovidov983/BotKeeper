namespace BotKeeper.Service {
	using BotKeeper.Service.Core.Helpers;
	using BotKeeper.Service.Core.Interfaces;
	using BotKeeper.Service.Core.Models;
	using BotKeeper.Service.Core.Services;

	using System;
	using Telegram.Bot;
	using Telegram.Bot.Args;
	internal class ApplicationBot : IDisposable {

		private readonly ITelegramBotClient client;
		private readonly IServiceFactory serviceFactory;
		private readonly ICommandHandlerFactory handlerFactory;
		private readonly IContextFactory contextFactory;
		private readonly ILogger logger;
		private readonly Settings settings;

		public ApplicationBot(ITelegramBotClient client, IStorage storage, Settings settings, ILogger logger) {
			this.client = client ?? throw new ArgumentNullException(nameof(client));

			serviceFactory = new ServiceFactory(storage, client, Settings.Logger);
			handlerFactory = serviceFactory.HandlerFactory;
			contextFactory = serviceFactory.ContextFactory;

			this.logger = logger;
			this.settings = settings;
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
				var context = await contextFactory.CreateContext(request);
				var userTextMessage = request.GetClearedTextMessage();
				var handler = handlerFactory.CreateHandlerForCommand(userTextMessage);

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

			/// We assume that in the production version you do not need to send an error message to the chat
			if (settings.IsProd) {
				SendShortError(request);
			} else {
				SendFullError(request, ex);
			}
		}

		private void SendShortError(MessageEventArgs request) {
			var sender = serviceFactory.SenderFactory.CreateSender(request);
			sender.Send($"Something went wrong...");
		}

		private void SendFullError(MessageEventArgs request, BotException ex) {
			var sender = serviceFactory.SenderFactory.CreateSender(request);
			sender.Send($"Unhandled error: {ex.Message}", request);
		}

		public void Dispose() {
			Stop();
		}
		public void Stop() {
			client?.StopReceiving();
		}
	}
}