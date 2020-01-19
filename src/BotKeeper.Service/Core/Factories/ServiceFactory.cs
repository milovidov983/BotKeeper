using BotKeeper.Service.Core.Factories;
using BotKeeper.Service.Core.Interfaces;
using Telegram.Bot;

namespace BotKeeper.Service.Core.Services {
	internal class ServiceFactory : IServiceFactory {
		private readonly IStorage storage;
		private readonly ILogger logger;
		private readonly ICommandHandlerFactory parserService;
		private readonly IUserService userService;
		private readonly ISender sender;
		private readonly IContextFactory contextFactory;
		private readonly IEmegencyService emegencyService;
		private readonly ISenderFactory senderFactory;

		public ICommandHandlerFactory HandlerFactory => parserService;
		public IUserService UserService => userService;
		public IStorage Storage => storage;
		public ISender Sender => sender;
		public ILogger Logger => logger;
		public IContextFactory ContextFactory => contextFactory;
		public IEmegencyService EmegencyService => emegencyService;
		public ISenderFactory SenderFactory => senderFactory;

		public ServiceFactory(IStorage storage, ITelegramBotClient client, ILogger logger) {
			this.storage = storage;
			this.logger = logger;

			var metricFactory = new MetricsFactory(Settings.Instance.Env, Settings.Logger);
			var metricService = metricFactory.Create();

			senderFactory = new SenderFactory(client, metricService);
			parserService = new CommandHandlerFactory();
			userService = new UserService(storage, logger);

			var stateFactory = new StateFactory(logger);
			contextFactory = new ContextFactory(storage, userService, stateFactory, this);

			var emegencyServiceFactory = new EmegencyServiceFactory(Settings.Instance.Env, logger, sender);
			emegencyService = emegencyServiceFactory.Create();
		}
	}
}
