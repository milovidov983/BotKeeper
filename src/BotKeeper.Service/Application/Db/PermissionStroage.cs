using BotKeeper.Service.Application.Interfaces;
using BotKeeper.Service.Domain.Interfaces;
using BotKeeper.Service.Domain.Models;
using BotKeeper.Service.Domain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Application.Db {
	public class PermissionStroage : IPermissionStorage {
		private IStorage storage;
		private const string prefix = "__system__";
		public PermissionStroage(IStorage storage) {
			this.storage = storage;
		}
		public async Task<IPermission[]> GetAllPermissions(int userId) {
			await Task.Yield();
			var user = await storage.Get<User>(userId, $"{prefix}.{nameof(User)}");
			return user.Accesses.Permissions;
		}


	}
}
