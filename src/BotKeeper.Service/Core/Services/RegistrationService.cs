using BotKeeper.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Core.Services {
	internal class RegistrationService : IRegistrationService {
		private readonly IStorage storage;

		public RegistrationService(IStorage storage) {
			this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
		}

		public Task<string> StartRegistrationFor(IUser user) {
			throw new NotImplementedException();
		}
	}
}
