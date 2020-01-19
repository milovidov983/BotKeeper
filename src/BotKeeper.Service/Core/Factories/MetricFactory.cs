using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.Services;

namespace BotKeeper.Service.Core.Factories {
	internal class MetricsFactory : IMetricsFactory {
		private readonly FactoryHelper<IMetricsService> helper;

		public MetricsFactory(string env, ILogger logger) {
			var defaultService = new TestMetricsService();
			helper = new FactoryHelper<IMetricsService>(defaultService, logger, env);

			helper.InitFactory(Settings.AllEnvironments.Test, defaultService);
			helper.InitFactory(Settings.AllEnvironments.Develop, defaultService);
			helper.InitFactory(Settings.AllEnvironments.Production, new ProdMetricsService());
		}

		public IMetricsService Create() {
			return helper.CreateService();
		}
	}
}

