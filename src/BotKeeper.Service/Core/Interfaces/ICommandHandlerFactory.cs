using BotKeeper.Service.Core.Services;

namespace BotKeeper.Service.Core.Interfaces {
	internal interface ICommandHandlerFactory {
		ICommandHandler CreateHandlerForCommand(string command);
	}
}