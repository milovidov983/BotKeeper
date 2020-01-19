using BotKeeper.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Core.Interfaces.UsersInterfaces {
	internal interface IRegistrationContextUserService : IUserService {
		Task<RegisteredUser> Get(long id);
		Task<bool> IsUserExist(long id);
		Task<bool> CreateNewAccount(int userId);
	}
}
