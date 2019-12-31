using BotKeeper.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Models.Users {
	internal class Admin : IUser {
		private IPersistedUser persistedUser;

		public Admin(IPersistedUser persistedUser) {
			this.persistedUser = persistedUser;
		}
	}
}
