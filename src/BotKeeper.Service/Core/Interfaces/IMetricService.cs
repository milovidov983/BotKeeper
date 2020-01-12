using System.Collections.Generic;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Services {

	internal interface IMetricsService {
		Dictionary<string, object> CreateMetricsFrom(string textMessage, MessageEventArgs request);
	}
}