using BotKeeper.Service.Core.interfaces;
using BotKeeper.Service.Core.Models;
using System;
using System.Threading.Tasks;

namespace BotKeeper.Service.Core.Services {
	internal class UserService : IUserService {
		private IStorage storage;

		public UserService(IStorage storage) {
			this.storage = storage 
				?? throw new ArgumentNullException(nameof(storage));
		}

		public async Task<User> Get(long id) {
			var persistedUser = await storage.GetUser(id);
			if (persistedUser.HasResult) {
				return persistedUser.Result;
			}
			return new User { Type = UserType.Guest };
		}

		public async Task<bool> IsUserExist(long id) {
			return await storage.IsUserExist(id);
		}
	}
}
