using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.Services;
using System;

namespace BotKeeper.Service.Core.Factories {
	internal class MetricsFactory : IMetricsFactory {
		private IMetricsService metricsService;
		private readonly string env;
		private readonly ILogger logger;

		public MetricsFactory(string env, ILogger logger) {
			this.env = env;
			this.logger = logger;
		}

		public IMetricsService CreateMetricsService() {
			if (metricsService is null) {
				metricsService = Create();
			}
			return metricsService;
		}

		private IMetricsService Create() {
			return env switch
			{
				"test" => new TestMetricsService(),
				"prod" => new ProdMetricsService(),
				"develop" => new TestMetricsService(),
				_ =>  CreateDefaultMetricsService()
				
			};
		}

		private IMetricsService CreateDefaultMetricsService() {
			var defaultMetricsService = new TestMetricsService();
			LogDefaultCreation(defaultMetricsService);

			return new TestMetricsService();
		}

		private void LogDefaultCreation(IMetricsService defaultMetricsService) {
			var defaultMetricsServiceType = defaultMetricsService.GetType();
			var mainLogInfo = $"Default metrics service ( {defaultMetricsServiceType.Name} ) selected. ";

			var additionalLogMessage = IsEnvVariableNotSet()
				? $"Env( {nameof(Settings)}.{nameof(Settings.Instance.Env)} <-- ) variable not set."
				: $"Env( {nameof(Settings)}.{nameof(Settings.Instance.Env)} <-- ) variable has an invalid value: [ {env} <-- ]";


			logger.Warn(mainLogInfo + additionalLogMessage);
		}

		private bool IsEnvVariableNotSet() {
			return string.IsNullOrEmpty(env);
		}
	}
}

