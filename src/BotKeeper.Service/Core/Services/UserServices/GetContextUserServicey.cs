using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.Interfaces.UsersInterfaces;
using BotKeeper.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Core.Services {
	internal class GetContextUserService : IGetContextUserService {
		private readonly IStorage storage;
		public GetContextUserService(IStorage storage) {
			this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
		}

		public async Task<UserKeeper> Get(long id) {
			await Task.Yield();
			var mock = new UserKeeper {
				Id = id
			};
			return mock;
		}

		public async Task<string> GetLast(long userId) {
			var key = await GetUserLastKey(userId);
			if(key is null) {
				return null;
			}

			var data = await storage.Get<string>(userId, key);
			if (data.HasResult) {
				return data.Result;
			}

			return null;
		}

		private async Task<string> GetUserLastKey(long userId) {
			var persistedUser = await storage.GetUser(userId);
			if (!persistedUser.HasResult) {
				throw new ApplicationException($"User {userId} not found!");
			}
			return persistedUser.Result.StorageData?.LastKey;
		}
	}
}
