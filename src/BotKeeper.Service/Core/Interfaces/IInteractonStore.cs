using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Interfaces {
	internal interface IInteractonStore {
		Task<IInteraction> Get(BaseUser user, long id);
	}
}
