using BotKeeper.Service.Core.Models;
using System;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Services {
	internal class ProdMetricsService : IMetricsService {
		public string CurrentState {
			get => throw new NotImplementedException();
			set => throw new NotImplementedException();
		}

		public IMetrics CreateMetricsFrom(string textMessage, MessageEventArgs request) {
			return null;
		}
	}
}
