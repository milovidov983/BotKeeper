using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Infrastructure.Interfaces {
	interface IUserFactory {
		Task<IUser> Create(int id);
	}
}
