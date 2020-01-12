using BotKeeper.Service.Core.Services;

namespace BotKeeper.Service.Core.Interfaces {
	internal interface IStratagyRepository {
		ICommandHandlerStratagy GetStratagyForCommand(string commandText);
	}
}