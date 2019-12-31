using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Interfaces {
	internal interface IInteractor {
		Task Execute(IMessage message);
	}
}
