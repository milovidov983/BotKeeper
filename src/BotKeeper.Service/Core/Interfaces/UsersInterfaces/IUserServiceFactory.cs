using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Core.Interfaces.UsersInterfaces {
	internal interface IUserServiceFactory {
		IUserService CreateUserService(AbstractStateDefault currentState);
	}
}
