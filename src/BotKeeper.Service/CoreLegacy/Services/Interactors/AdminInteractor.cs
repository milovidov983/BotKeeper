using BotKeeper.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Services.Interactors {
	internal class AdminInteractor : IInteractor {
		private BaseUser user;
		private IBotClient client;
		private readonly IInteractonFactory interactionStore;

		public AdminInteractor(BaseUser user, IBotClient client, IInteractonFactory interactionStore) {
			this.user = user;
			this.client = client;
			this.interactionStore = interactionStore;
		}

		public Task Execute(IMessage message) {
			throw new NotImplementedException();
		}
	}
}
