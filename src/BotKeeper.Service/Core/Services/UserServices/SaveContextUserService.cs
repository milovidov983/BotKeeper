using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.Interfaces.UsersInterfaces;
using BotKeeper.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Core.Services {
	internal class SaveContextUserService : ISaveContextUserService {
		private readonly IStorage storage;
		public SaveContextUserService(IStorage storage) {
			this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
		}

		public async Task<UserKeeper> Get(long id) {
			await Task.Yield();
			var mock = new UserKeeper {
				Id = id
			};
			return mock;
		}

		public async Task<string> SaveUserData(long userId, string data) {
			string newKey = await GetUserDefinedKey(userId) ?? Guid.NewGuid().ToString();
			await storage.Save(userId, newKey, data);
			return newKey;
		}

		private async Task<string> GetUserDefinedKey(long userId) {
			var persistedUser = await storage.GetUser(userId);
			if (!persistedUser.HasResult) {
				throw new ApplicationException($"User {userId} not found!");
			}
			return persistedUser.Result.StorageData?.UserDefinedKey;
		}
	}
}
