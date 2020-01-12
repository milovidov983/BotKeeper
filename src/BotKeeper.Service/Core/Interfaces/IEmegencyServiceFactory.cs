using BotKeeper.Service.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Core.Factories {
	internal interface IEmegencyServiceFactory {
		IEmegencyService Create();
	}
}
