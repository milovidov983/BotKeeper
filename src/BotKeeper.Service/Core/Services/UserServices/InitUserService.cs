using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.Interfaces.UsersInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Core.Services.UserServices {
	internal class InitUserService : IInitUserService {
		private readonly IStorage storage;
		public InitUserService(IStorage storage) {
			this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
		}
		public async Task<string> GetUserState(long userId) {
			var storedUserState = await storage.GetUserState(userId);
			if (storedUserState.HasResult) {
				return storedUserState.Result;
			} else {
				return default;
			}
		}

		public async Task<bool> IsUserExist(long userId) {
			return await storage.IsUserExist(userId);
		}
	}
}
