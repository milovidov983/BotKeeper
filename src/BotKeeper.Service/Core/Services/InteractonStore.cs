using BotKeeper.Service.Interfaces;
using BotKeeper.Service.Persistence.Db;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Core.Services {
	internal class InteractonStore : IInteractonStore {
		private readonly IStorage storage;

		public InteractonStore(IStorage storage) {
			this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
		}

		public Task<IInteraction> Get(BaseUser user, long id) {
			throw new NotImplementedException();
		}
	}
}
