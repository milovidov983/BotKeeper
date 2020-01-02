using BotKeeper.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Models.Users  {
	internal class UnknownUser : IUser {
		private int id;

		public UnknownUser(int id) {
			this.id = id;
		}

		public int Id => id;
	}
}
