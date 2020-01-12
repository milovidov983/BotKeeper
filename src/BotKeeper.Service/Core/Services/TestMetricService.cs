using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Services {
	internal class TestMetricsService : IMetricsService {

		public Dictionary<string, object> CreateMetricsFrom(string textMessage, MessageEventArgs request) {
			return new Dictionary<string, object> {
				{ nameof(textMessage), textMessage},
				{ nameof(request), JsonConvert.SerializeObject(request)}
			};
		}
	}
}
