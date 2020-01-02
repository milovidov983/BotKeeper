using BotKeeper.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Models.Users {
	internal class Admin : BaseUser {
		private readonly IPersistedUser persistedUser;
		private readonly int id;

		public Admin(IPersistedUser persistedUser) {
			this.persistedUser = persistedUser;
			id = persistedUser.Id;
		}

		public override int Id { get => id; }
	}
}
