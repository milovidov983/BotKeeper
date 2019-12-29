using BotKeeper.Service.Application.Db;
using BotKeeper.Service.Application.Interfaces;
using BotKeeper.Service.Domain.Interfaces;
using BotKeeper.Service.Domain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Application {
	public class StorageAgent : IStorageAgent {
		private IPermissionStorageFactory permissionStorageFactory;
		public StorageAgent(IStorage storage) {
			this.permissionStorageFactory = new PermissionStroageFactory(storage);
		}
		public async Task<IPermission[]> GetPermissionFor(int userId, EntityType entityType) {
			await Task.Yield();
			var storage = await permissionStorageFactory.CreateStorageFor(userId, entityType);
			return storage.GetPermissions();
		}
	}
}
