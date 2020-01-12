using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.Models;
using System;
using System.Threading.Tasks;

namespace BotKeeper.Service.Core.Services {
	internal class UserService : IUserService {
		private readonly IStorage storage;
		private readonly ILogger logger;

		public UserService(IStorage storage, ILogger logger) {
			this.storage = storage 
				?? throw new ArgumentNullException(nameof(storage));
			this.logger = logger;
		}

		public async Task<bool> CreateNewAccount(int userId) {
			await storage.Save(userId);
			return true;
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
