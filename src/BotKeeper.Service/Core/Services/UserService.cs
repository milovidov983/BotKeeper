using BotKeeper.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Core.Services {
    internal class UserService {
		private IStorage storage;

		public UserService(IStorage storage) {
			this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
		}

		public User Get(long id) {


            return new User { Type = UserType.Guest };
        }

		public bool IsUserExist(long id) {
			return storage.IsUserExist(id);
		}
	}
}
