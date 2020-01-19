using BotKeeper.Service.Core.Factories;
using BotKeeper.Service.Core.Factories.Users;
using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.Interfaces.UsersInterfaces;
using Telegram.Bot;

namespace BotKeeper.Service.Core.Services {
	internal class ServiceFactory : IServiceFactory {
		private readonly IStorage storage;
		private readonly ILogger logger;
		private readonly ICommandHandlerFactory parserService;
		private readonly IContextFactory contextFactory;
		private readonly ISenderFactory senderFactory;
		private readonly IUserServiceFactory userServiceFactory;
		private readonly IStateFactory stateFactory;


		public ICommandHandlerFactory HandlerFactory => parserService;
		public IStorage Storage => storage;
		public ILogger Logger => logger;
		public IContextFactory ContextFactory => contextFactory;
		public ISenderFactory SenderFactory => senderFactory;

		public IUserServiceFactory UserServiceFactory => userServiceFactory;

		public IStateFactory StateFactory => stateFactory;

		public ServiceFactory(IStorage storage, ITelegramBotClient client, ILogger logger) {
			this.storage = storage;
			this.logger = logger;

			userServiceFactory = new UserServiceFactory(storage, logger);
			var metricFactory = new MetricsFactory(Settings.Instance.Env, Settings.Logger);
			var metricService = metricFactory.Create();

			senderFactory = new SenderFactory(client, metricService);
			parserService = new CommandHandlerFactory();

			stateFactory = new StateFactory(logger);
			contextFactory = new ContextFactory(storage, userServiceFactory, stateFactory, this);

		}
	}
}
