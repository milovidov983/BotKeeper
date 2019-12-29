using BotKeeper.Service.Application.Interfaces;
using BotKeeper.Service.Domain.Interfaces;
using BotKeeper.Service.Domain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BotKeeper.Service.Application.Db {
	// TODO inherit in the unit test and check the quantity with the number of descendants
	// Type ourtype = typeof(EntityType);
	// var orderTypes = Assembly.GetAssembly(ourtype).GetTypes().Where(type => type.IsSubclassOf(ourtype)).ToArray();
	internal class PermissionStroageFactory : IPermissionStorageFactory {
		protected Dictionary<Type, IPermissionStorage> permissionStorages = new Dictionary<Type, IPermissionStorage>();
		private IStorage storage;
		public PermissionStroageFactory(IStorage storage) {
			permissionStorages.Add(typeof(ChatEntity), new ChatEntityPermitionStorage(storage));
		}

		public async Task<IPermissionStorage> CreateStorageFor(int userId, EntityType entityType) {
			await Task.Yield();
			var type = entityType.GetType();

			if (permissionStorages.TryGetValue(type, out var store)) {
				return store.GetStoreFor(userId);
			}

			throw new Exception($"Unknown type {type}");
		}
	}
}