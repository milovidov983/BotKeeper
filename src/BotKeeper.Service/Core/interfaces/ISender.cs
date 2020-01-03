using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Interfaces {
	internal interface ISender {
		void Send(string text, MessageEventArgs request);
	}
}