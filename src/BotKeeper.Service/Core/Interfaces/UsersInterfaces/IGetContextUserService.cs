using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Core.Interfaces.UsersInterfaces {
	internal interface IGetContextUserService : IUserService {
		Task<string> GetLast(long userId);
	}
}