using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.Interfaces.UsersInterfaces;
using BotKeeper.Service.Core.Models;
using System;
using System.Threading.Tasks;

namespace BotKeeper.Service.Core.Services {
	internal class RegistrationContextUserService : IRegistrationContextUserService {
		private readonly IStorage storage;
		private readonly ILogger logger;

		public RegistrationContextUserService(IStorage storage, ILogger logger) {
			this.storage = storage
				?? throw new ArgumentNullException(nameof(storage));
			this.logger = logger;
		}

		public async Task<bool> CreateNewAccount(int userId) {
			await storage.CreateNewUser(userId);
			return true;
		}

		public async Task<RegisteredUser> Get(long id) {
			var persistedUser = await storage.GetUser(id);
			if (persistedUser.HasResult) {
				var registerationData = persistedUser.Result.RegisterationData;
				return new RegisteredUser {
					Id = persistedUser.Result.Id,
					Name = registerationData.Name,
					Secret = registerationData.Secret,
					Type = registerationData.Type
				};
			}
			return new RegisteredUser { Type = UserType.Guest };
		}

		public async Task<bool> IsUserExist(long id) {
			return await storage.IsUserExist(id);
		}
	}
}
