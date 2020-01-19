using BotKeeper.Service.Core.Interfaces;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Factories {
	internal interface ISenderFactory {
		ISender CreateSender(MessageEventArgs request);
	}
}