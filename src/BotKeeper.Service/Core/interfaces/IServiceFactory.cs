using BotKeeper.Service.Core.interfaces;

namespace BotKeeper.Service.Core.interfaces {
	internal interface IServiceFactory {
		IParserService ParserService { get; }
		ISender Sender { get; }
		IStorage Storage { get; }
		IUserService UserService { get; }
	}
}