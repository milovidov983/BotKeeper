using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Interfaces {
	internal interface IRegistrationService {
		Task<string> StartRegistrationFor(BaseUser user);
	}
}
