using BotKeeper.Service.Domain.Models.Entity;
using System.Threading.Tasks;

namespace BotKeeper.Service.Domain.Interfaces {
	public interface IMember {
		Task<IPermission> GetPermitionStatusFor(int userId, EntityType entityType);
	}
}