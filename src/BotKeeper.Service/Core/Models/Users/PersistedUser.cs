using BotKeeper.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Core.Models.Users {
	internal class PersistedUser : IPersistedUser {

		public string Data { get; set; }
		public int Id { get ; set; }
		public string Type { get; set; }
	}
}
