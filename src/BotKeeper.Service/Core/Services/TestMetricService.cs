using BotKeeper.Service.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
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
