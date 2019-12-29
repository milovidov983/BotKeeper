using BotKeeper.Service.Domain.Interfaces;
using BotKeeper.Service.Domain.Models.Entity;
using BotKeeper.Service.Domain.Models.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Domain.Services {
	public class MemberService : IMember {
		private IStorageAgent storageAgent;
		public async Task<IPermission> GetPermitionStatusFor(int userId, EntityType entityType) {
			IPermissionStorage userPermissionStorage = await storageAgent.GetPermissionStorageFor(entityType);
			var userPermissions = await userPermissionStorage.GetAllPermissions(userId);

			var result = entityType switch
			{
				ChatEntity _ => userPermissions.FirstOrDefault(permission => permission is ChatEntity),
				_ => throw new NotImplementedException("Unknown type of entity")
			};
			
			if(result is null) {
				return EmptyPermission.Instance;
			}
			return result;
		}
	}
}
