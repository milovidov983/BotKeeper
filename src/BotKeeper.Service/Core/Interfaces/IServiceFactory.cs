using BotKeeper.Service.Interfaces;

namespace BotKeeper.Service.Core.Models {
	internal interface IServiceFactory {
		IRegistrationService CreateRegistrationService();
	}
}