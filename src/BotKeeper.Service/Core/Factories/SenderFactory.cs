using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Factories {
	internal class SenderFactory : ISenderFactory {

		private readonly ITelegramBotClient client;
		private readonly IMetricsService metricService;

		public SenderFactory(ITelegramBotClient client, IMetricsService metricService) {
			this.client = client ?? throw new ArgumentNullException(nameof(client));
			this.metricService = metricService;
		}
		public ISender CreateSender(MessageEventArgs request) {
			return new SenderService(client, metricService, request);
		}
	}
}
