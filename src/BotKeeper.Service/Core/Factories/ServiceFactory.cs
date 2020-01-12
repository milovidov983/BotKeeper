using BotKeeper.Service.Core.Factories;
using BotKeeper.Service.Core.Interfaces;

namespace BotKeeper.Service.Core.Services {
	internal class ServiceFactory : IServiceFactory {
		private readonly IStorage storage;
		private readonly ILogger logger;
		private readonly IStratagyRepository parserService;
		private readonly IUserService userService;
		private readonly ISender sender;
		private readonly IContextFactory contextFactory;
		private readonly IEmegencyService emegencyService;

		public IStratagyRepository HandlerFactory => parserService;
		public IUserService UserService => userService;
		public IStorage Storage => storage;
		public ISender Sender => sender;
		public ILogger Logger => logger;
		public IContextFactory ContextFactory => contextFactory;
		public IEmegencyService EmegencyService => emegencyService;

		public ServiceFactory(IStorage storage, ISender sender, ILogger logger) {
			this.sender = sender;
			this.storage = storage;
			this.logger = logger;

			parserService = new StratagyRepository();
			userService = new UserService(storage, logger);

			var stateFactory = new StateFactory(logger);
			contextFactory = new ContextFactory(storage, userService, stateFactory, this);

			var emegencyServiceFactory = new EmegencyServiceFactory(Settings.Instance.Env, logger, sender);
			emegencyService = emegencyServiceFactory.Create();
		}
	}
}
