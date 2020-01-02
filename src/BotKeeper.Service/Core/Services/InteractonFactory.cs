using BotKeeper.Service.Interfaces;
using BotKeeper.Service.Persistence.Db;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Core.Services {
	internal class InteractonFactory : IInteractonFactory {
		private readonly IStorage storage;

		public InteractonFactory(IStorage storage) {
			this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
		}

		public Task<IInteraction> Get(BaseUser user) {
			throw new NotImplementedException();
		}
	}
}