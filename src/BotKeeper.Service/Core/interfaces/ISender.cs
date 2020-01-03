using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.interfaces {
	internal interface ISender {
		void Send(string text, MessageEventArgs request);
	}
}