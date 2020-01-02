using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Interfaces {
	internal interface IInteractonFactory {
		Task<IInteraction> Get(BaseUser user);
	}
}
