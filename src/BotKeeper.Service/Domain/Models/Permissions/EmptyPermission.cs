using BotKeeper.Service.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Domain.Models.Permissions {
	public class EmptyPermission : IPermission {
		public static EmptyPermission Instance;

		static EmptyPermission() {
			Instance = new EmptyPermission();
		}
	}
}
