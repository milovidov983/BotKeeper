using BotKeeper.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Core.Interfaces.UsersInterfaces {
	internal interface ISaveContextUserService : IUserService {
		Task<UserKeeper> Get(long id);
		Task<string> SaveUserData(long userId, string data);
	}
}
