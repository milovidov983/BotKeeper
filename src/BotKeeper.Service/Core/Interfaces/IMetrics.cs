using System.Collections.Generic;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Models {
	public interface IMetrics {
		string CurrentState { get; set; }
		string Message { get; set; }
		MessageEventArgs Request { get; set; }


		Dictionary<string, object> ToDictionary();
	}
}