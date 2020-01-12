using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.Services;

namespace BotKeeper.Service.Core.Interfaces {
	internal interface IServiceFactory {
		IStratagyRepository HandlerFactory { get; }
		ISender Sender { get; }
		IStorage Storage { get; }
		IUserService UserService { get; }
		ILogger Logger { get; }
		IContextFactory ContextFactory { get; }
		IEmegencyService EmegencyService { get; }
	}
}