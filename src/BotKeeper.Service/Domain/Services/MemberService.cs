using BotKeeper.Service.Domain.Interfaces;
using BotKeeper.Service.Domain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Domain.Services {
	public class MemberService : IMember {
		private IStorageAgent storageAgent;
		public async Task<IPermission> GetPermitionStatusFor(int userId, EntityType entityType) {
			IPermissionStorage userPermissionStorage = await storageAgent.GetPermissionStorageFor(userId);
			return await userPermissionStorage.GetPermisionFor(entityType);
		}
	}
}
