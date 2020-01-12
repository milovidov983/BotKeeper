using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.Services;
using System;

namespace BotKeeper.Service.Core.Factories {
	internal class MetricsFactory : IMetricsFactory {
		private readonly FactoryHelper<IMetricsService> helper;

		public MetricsFactory(string env, ILogger logger) {
			var defaultService = new TestMetricsService();
			helper = new FactoryHelper<IMetricsService>(defaultService, logger, env);

			helper.InitFactory(Settings.Environments.Test, defaultService);
			helper.InitFactory(Settings.Environments.Develop, defaultService);
			helper.InitFactory(Settings.Environments.Production, new ProdMetricsService());
		}

		public IMetricsService Create() {
			return helper.CreateService();
		}
	}
}

