using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Interfaces {
	internal interface IInteractonStore {
		Task<IInteraction> Get(IUser user, long id);
	}
}
