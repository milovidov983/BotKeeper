using BotKeeper.Service.Domain.Models.Entity;
using System.Threading.Tasks;

namespace BotKeeper.Service.Domain.Interfaces {
	internal interface IStorageAgent {
		Task<IPermission[]> GetPermissionFor(int userId, EntityType entityType);
	}
}