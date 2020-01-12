using BotKeeper.Service.Core.Services;

namespace BotKeeper.Service.Core.Interfaces {
	internal interface IHandlerFactory {
		IHandlerClient GetHandlerForCommand(string commandText);
	}
}