using BotKeeper.Service.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Application.Interfaces {
	interface IInteractor {
		Task Execute(IMessage message);
	}
}
