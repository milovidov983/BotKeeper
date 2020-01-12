using BotKeeper.Service.Core.Helpers;
using BotKeeper.Service.Core.Interfaces;
using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Services {
	internal class SenderService : ISender {

		private readonly TelegramBotClient client;
		private readonly IMetricsService metricService;

		public SenderService(TelegramBotClient client, IMetricsService metricService) {
			this.client = client ?? throw new ArgumentNullException(nameof(client));
			this.metricService = metricService;
		}
		public void Send(string textMessage, MessageEventArgs request) {
			var metricData = metricService.CreateMetricsFrom(textMessage, request);
			
			Ext.SafeRun(async () => 
				await client.SendTextMessageAsync(request.Message.From.Id, textMessage), 
				metricData.ToDictionary()
			);
		}
	}
}
