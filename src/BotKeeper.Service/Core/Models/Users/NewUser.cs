using BotKeeper.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Core.Models.Users {
	internal class NewUser : BaseUser {
		private readonly int id;
		private readonly IPersistedUser persistedUser;

		public NewUser(IPersistedUser persistedUser) {
			this.persistedUser = persistedUser;
			id = persistedUser.Id;
		}


		public override int Id => id;
	}
}
