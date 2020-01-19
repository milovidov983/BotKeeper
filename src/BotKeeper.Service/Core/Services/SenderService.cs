using BotKeeper.Service.Core.Helpers;
using BotKeeper.Service.Core.Interfaces;
using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Services {
	internal class SenderService : ISender {

		private readonly ITelegramBotClient client;
		private readonly IMetricsService metricService;
		private readonly MessageEventArgs request;

		public SenderService(ITelegramBotClient client, IMetricsService metricService, MessageEventArgs request) {
			this.client = client ?? throw new ArgumentNullException(nameof(client));
			this.metricService = metricService;
			this.request = request;
		}
		public void Send(string response, MessageEventArgs request = null) {
			var req = request ?? this.request ?? throw new ArgumentNullException(nameof(request));

			Ext.SafeRun(async () =>
				await client.SendTextMessageAsync(req.Message.From.Id, response),
				metricService,
				response,
				request
			);
		}
	}
}
