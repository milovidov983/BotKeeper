using BotKeeper.Service.Core.Factories;
using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.Services;

namespace BotKeeper.Service.Core.Interfaces {
	internal interface IServiceFactory {
		ICommandHandlerFactory HandlerFactory { get; }
		ISender Sender { get; }
		IStorage Storage { get; }
		IUserService UserService { get; }
		ILogger Logger { get; }
		IContextFactory ContextFactory { get; }
		IEmegencyService EmegencyService { get; }

		ISenderFactory SenderFactory { get; set; }
	}
}