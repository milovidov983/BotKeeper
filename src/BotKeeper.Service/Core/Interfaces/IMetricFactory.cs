using BotKeeper.Service.Core.Services;

namespace BotKeeper.Service.Core.Factories {
	internal interface IMetricsFactory {
		IMetricsService Create();
	}
}