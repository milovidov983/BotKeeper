using BotKeeper.Service.Core.Services;

namespace BotKeeper.Service.Core.Interfaces {
	internal interface ICommandHandlerFactory {
		ICommandHandler CreateHandler(string command);
		ICommandHandler CreateHandlerForCommand(BotContext context, string userTextMessage);
	}
}