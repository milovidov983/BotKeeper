﻿using BotKeeper.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Models.Users {
	internal class Admin : IUser {
		private IPersistedUser persistedUser;
		private int id;

		public Admin(IPersistedUser persistedUser) {
			this.persistedUser = persistedUser;
		}

		public int Id { get => id; }
	}
}