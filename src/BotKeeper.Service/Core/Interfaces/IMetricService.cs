using BotKeeper.Service.Core.Models;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Services {

	internal interface IMetricsService {
		IMetrics CreateMetricsFrom(string textMessage, MessageEventArgs request);
		string CurrentState { get; set; }

	}
}