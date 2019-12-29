using BotKeeper.Service.Application.Interfaces;
using BotKeeper.Service.Domain.Interfaces;
using BotKeeper.Service.Domain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Application.Db {
	public class PermissionStroage : IPermissionStorage {
		private IStorage storage;
		private IPermissionStorageFactory permissionStorageFactory;
		public PermissionStroage(IStorage storage) {
			this.storage = storage;
			this.permissionStorageFactory = new PermissionStroageFactory();
		}

		public Task<IPermission> GetPermisionFor(EntityType entityType) {
			
		}
	}
}
