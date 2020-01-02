using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Senders {
	internal interface ISender {
		void Send(string text, MessageEventArgs request);
	}
}