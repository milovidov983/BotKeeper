using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Core.Interfaces.UsersInterfaces {
	internal interface IInitUserService : IUserService {
		Task<string> GetUserState(long id);
		Task<bool> IsUserExist(long id);
	}
}
