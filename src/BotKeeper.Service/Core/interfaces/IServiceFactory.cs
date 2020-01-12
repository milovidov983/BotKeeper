using BotKeeper.Service.Core.Interfaces;

namespace BotKeeper.Service.Core.Interfaces {
	internal interface IServiceFactory {
		IHandlerFactory HandlerFactory { get; }
		ISender Sender { get; }
		IStorage Storage { get; }
		IUserService UserService { get; }
		IHandlerService HandlerService { get; }
		ILogger Logger { get; }
		IContextFactory ContextFactory { get; }
	}
}