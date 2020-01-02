using BotKeeper.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Services.Interactors {
	internal class UserInteractor : IInteractor {
		private IUser user;
		private IBotClient client;
		private readonly IInteractonStore interactionStore;

		public UserInteractor(IUser user, IBotClient client, IInteractonStore interactionStore) {
			this.user = user;
			this.client = client;
			this.interactionStore = interactionStore;
		}

		public Task Execute(IMessage message) {
			throw new NotImplementedException();
		}
	}
}
