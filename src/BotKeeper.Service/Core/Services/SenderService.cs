using BotKeeper.Service.Core.Helpers;
using BotKeeper.Service.Core.Interfaces;
using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Services {
	internal class SenderService : ISender {

		private readonly TelegramBotClient client;
		private readonly IMetricsService metricService;
		private readonly MessageEventArgs request;

		public SenderService(TelegramBotClient client, IMetricsService metricService, MessageEventArgs request) {
			this.client = client ?? throw new ArgumentNullException(nameof(client));
			this.metricService = metricService;
			this.request = request;
		}
		public void Send(string textMessage) {
			var metricData = metricService.CreateMetricsFrom(textMessage, request);
			
			Ext.SafeRun(async () => 
				await client.SendTextMessageAsync(request.Message.From.Id, textMessage), 
				metricData.ToDictionary()
			);
		}
	}
}
