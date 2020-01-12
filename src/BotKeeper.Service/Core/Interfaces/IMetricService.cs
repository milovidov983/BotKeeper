using BotKeeper.Service.Core.Models;
using System.Collections.Generic;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Services {

	internal interface IMetricsService {
		IMetrics CreateMetricsFrom(string textMessage, MessageEventArgs request);
		string CurrentState { get; set; }

	}
}