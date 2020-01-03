using BotKeeper.Service.Core.Models;
using System.Threading.Tasks;

namespace BotKeeper.Service.Core.interfaces {
	internal interface IUserService {
		Task<User> Get(long id);
		Task<bool> IsUserExist(long id);
	}
}