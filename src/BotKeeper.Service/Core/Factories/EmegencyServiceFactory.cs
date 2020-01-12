using BotKeeper.Service.Core.Interfaces;
using System.Collections.Generic;

namespace BotKeeper.Service.Core.Factories {
	internal class EmegencyServiceFactory : IEmegencyServiceFactory {
		private readonly FactoryHelper<IEmegencyService> helper;

		public EmegencyServiceFactory(string env, ILogger logger, ISender sender) {
			var defaultService = new TestEmegencyService(sender);
			helper = new FactoryHelper<IEmegencyService>(defaultService, logger, env);

			helper.InitFactory(Settings.Environments.Test, defaultService);
			helper.InitFactory(Settings.Environments.Develop, defaultService);
			helper.InitFactory(Settings.Environments.Production, new ProdEmegencyService(sender));
		}

		public IEmegencyService Create() {
			return helper.CreateService();
		}
	}
}