using BotKeeper.Service.Core.Models;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Services {
	internal class TestMetricsService : IMetricsService {

		public string CurrentState { get; set; }

		public IMetrics CreateMetricsFrom(string textMessage, MessageEventArgs request) {
			return new Metrics {
				CurrentState = "unknown (DEBUG)",
				Message = textMessage,
				Request = request
			};
		}
	}
}
