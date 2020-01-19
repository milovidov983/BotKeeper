using BotKeeper.Service.Core.Factories;
using BotKeeper.Service.Core.Interfaces.UsersInterfaces;

namespace BotKeeper.Service.Core.Interfaces {
	internal interface IServiceFactory {
		ICommandHandlerFactory HandlerFactory { get; }
		IStorage Storage { get; }
		IUserServiceFactory UserServiceFactory { get; }
		ILogger Logger { get; }
		IContextFactory ContextFactory { get; }
		ISenderFactory SenderFactory { get; }
		IStateFactory StateFactory { get; }
	}
}