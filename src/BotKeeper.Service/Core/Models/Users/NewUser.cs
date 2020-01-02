using BotKeeper.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Core.Models.Users {
	internal class NewUser : IUser {
		private int id;
		private IPersistedUser persistedUser;

		public NewUser(IPersistedUser persistedUser) {
			this.persistedUser = persistedUser;
		}


		public int Id => id;
	}
}
