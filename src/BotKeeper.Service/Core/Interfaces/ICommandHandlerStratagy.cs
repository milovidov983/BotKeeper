using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Services {
	internal interface ICommandHandlerStratagy {
		void Execute(BotContext context, MessageEventArgs request);
	}
}