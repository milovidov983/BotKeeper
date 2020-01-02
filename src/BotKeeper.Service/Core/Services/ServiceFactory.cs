using BotKeeper.Service.Core.Models;
using BotKeeper.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Core.Services {
	internal class ServiceFactory : IServiceFactory {
		private IStorage storage;

		public ServiceFactory(IStorage storage) {
			this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
		}

		public IRegistrationService CreateRegistrationService() {
			return new RegistrationService(storage);
		}
	}
}
