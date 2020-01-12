using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Services {
	internal interface ICommandHandler {
		void Execute(BotContext context, MessageEventArgs request);
	}
}