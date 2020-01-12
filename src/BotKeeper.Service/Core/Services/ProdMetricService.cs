using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Services {
	internal class ProdMetricsService : IMetricsService {
		public Dictionary<string, object> CreateMetricsFrom(string textMessage, MessageEventArgs request) {
			return null;
		}
	}
}
