using BotKeeper.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Models.Users {
	internal class User : IUser {
		private IPersistedUser persistedUser;

		public User(IPersistedUser persistedUser) {
			this.persistedUser = persistedUser;
		}
	}
}
