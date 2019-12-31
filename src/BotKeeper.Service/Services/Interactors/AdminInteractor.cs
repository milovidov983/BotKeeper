using BotKeeper.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Services.Interactors {
	internal class AdminInteractor : IInteractor {
		private IUser user;
		private IBotClient client;

		public AdminInteractor(IUser user, IBotClient client) {
			this.user = user;
			this.client = client;
		}

		public Task Execute(IMessage message) {
			throw new NotImplementedException();
		}
	}
}
