using BotKeeper.Service.Core.Interfaces;
using System.Collections.Generic;

namespace BotKeeper.Service.Core.Factories {
	internal class FactoryHelper<TService> {
		private readonly string env;
		private readonly ILogger logger;
		private readonly TService defaultService;
		private Dictionary<string, TService> services = new Dictionary<string, TService>();
		private TService service;


		public FactoryHelper(TService defaultService, ILogger logger, string env) {
			this.defaultService = defaultService;
			this.logger = logger;
			this.env = env ?? string.Empty;
		}

		public void InitFactory(string env, TService serviceInstance) {
			services.Add(env, serviceInstance);
		}

		public TService CreateService() {
			if (service is null) {
				service = Create();
			}
			return service;
		}

		private TService Create() {
			if (services.TryGetValue(env, out var serviceInstance)) {
				return serviceInstance;
			}
			return CreateDefaultService();
		}

		private TService CreateDefaultService() {
			LogDefaultCreation(defaultService);

			return defaultService;
		}

		private void LogDefaultCreation(TService defaultService) {
			var defaultServiceType = defaultService.GetType();
			var mainLogInfo = $"Default metrics service ( {defaultServiceType.Name} ) selected. ";

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
