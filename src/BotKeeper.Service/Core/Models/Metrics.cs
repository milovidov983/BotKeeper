using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Models {
	public class Metrics : IMetrics {
		public Metrics() {
		}

		public Metrics(string currentState, MessageEventArgs request, string message) {
			CurrentState = currentState ?? throw new ArgumentNullException(nameof(currentState));
			Request = request ?? throw new ArgumentNullException(nameof(request));
			Message = message ?? throw new ArgumentNullException(nameof(message));
		}

		public string CurrentState { get; set; }
		public MessageEventArgs Request { get; set; }
		public string Message { get; set; }


		public static implicit operator Dictionary<string, object>(Metrics metrics) {
			return new Dictionary<string, object>() {
					{ nameof(metrics.CurrentState), metrics.CurrentState},
					{ nameof(metrics.Message), metrics.Message},
					{ nameof(metrics.Request), JsonConvert.SerializeObject(metrics.Request)}
				};
		
		}


		public Dictionary<string, object> ToDictionary() {
			return this;
		}
	}
}
